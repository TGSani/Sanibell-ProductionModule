using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Dapper;
using System.Data.Odbc;

namespace Sanibell_ProductionModule.Repositories;

public class OdbcPlannerRepository : IPlannerRepository
{

    // connection string
    private readonly string _cs;
    public OdbcPlannerRepository(IConfiguration cfg)
    {
        _cs = cfg.GetConnectionString("DemoArt")
        ?? throw new InvalidOperationException("Connectionstring ontbreekt");
    }
    // open connection
    private async Task<OdbcConnection> OpenAsync(CancellationToken ct)
    {
        var conn = new OdbcConnection(_cs);
        await conn.OpenAsync(ct);
        return conn;
    }


    public async Task<List<Planning>> GetPlanningAsync(CancellationToken ct = default)
    {
        // New version of the SQL for better readability and performance 
        const string GetAllSqlNew = """
        WITH Stock AS (
            SELECT
                AL.ArtLocArtGid AS ArtGid,
                SUM(CASE 
                        WHEN M.MagCode IN (1, 2, 9) AND L.LocNaam <> 'Retour voorraad' 
                            THEN AL.ArtLocVoorraad 
                        ELSE 0 
                    END) AS TotalCurrentStockNL,
                SUM(CASE 
                        WHEN M.MagCode IN (5, 8) AND L.LocNaam <> 'Retour voorraad' 
                            THEN AL.ArtLocVoorraad 
                        ELSE 0 
                    END) AS TotalCurrentStockPL
            FROM KingSystem.tabArtikelLocatie AS AL
            INNER JOIN KingSystem.tabLocatie AS L 
                ON L.LocGid = AL.ArtLocLocGid
            INNER JOIN KingSystem.tabMagazijn AS M 
                ON M.MagGid = L.LocMagGid
            GROUP BY AL.ArtLocArtGid
        ),
        Recommended AS (
            SELECT
                ORR.OrrArtGid AS ArtGid,
                SUM(CASE 
                        WHEN ISNULL(ORR.OrrLeverDatum, ORK.OrkLeverdatum) <= DATEADD(DAY, 7, GETDATE()) 
                            THEN ORR.OrrAantalBesteld - ORR.OrrAantalGeleverd 
                        ELSE 0 
                    END) AS Recommended7Days,
                SUM(CASE 
                        WHEN ISNULL(ORR.OrrLeverDatum, ORK.OrkLeverdatum) <= DATEADD(DAY, 14, GETDATE()) 
                            THEN ORR.OrrAantalBesteld - ORR.OrrAantalGeleverd 
                        ELSE 0 
                    END) AS Recommended14Days,
                SUM(CASE 
                        WHEN ISNULL(ORR.OrrLeverDatum, ORK.OrkLeverdatum) <= DATEADD(DAY, 21, GETDATE()) 
                            THEN ORR.OrrAantalBesteld - ORR.OrrAantalGeleverd 
                        ELSE 0 
                    END) AS Recommended21Days
            FROM KingSystem.tabOrderRegel AS ORR
            INNER JOIN KingSystem.tabOrderKop AS ORK 
                ON ORR.OrrOrkGid = ORK.OrkGid
            WHERE ORK.OrkGoedgekeurd = 1
            GROUP BY ORR.OrrArtGid
        )
        SELECT
            RCP.RcptOmschrijving       AS ArticleDescription,
            ART.ArtCode                AS ArticleNumber,
            VR.VRART_Maat              AS Size,
            VR.VRART_Kleur             AS Color,
            CAST(ISNULL(S.TotalCurrentStockNL, 0) AS DECIMAL(5,0)) AS TotalCurrentStockNL,
            CAST(ISNULL(S.TotalCurrentStockPL, 0) AS DECIMAL(5,0)) AS TotalCurrentStockPL,
            ISNULL(R.Recommended7Days, 0)  AS Recommended7Days,
            ISNULL(R.Recommended14Days, 0) AS Recommended14Days,
            ISNULL(R.Recommended21Days, 0) AS Recommended21Days
        FROM KingSystem.tabReceptuur AS RCP
        INNER JOIN KingSystem.tabReceptuurEindproduct AS RCE 
            ON RCE.RcpeRcptGid = RCP.RcptGid
        INNER JOIN KingSystem.tabArtikel AS ART 
            ON ART.ArtGid = RCE.RcpeArtGid
        LEFT JOIN KingSystem.vrGetContent(
                    'ART',
                    0,
                    0,
                    'Maat
                    Kleur',
                    '',
                    ''
                )
                WITH (
                    VRART_Gid   INT,
                    VRART_Maat  NCHAR(40),
                    VRART_Kleur NCHAR(40)
                ) AS VR 
            ON VR.VRART_Gid = ART.ArtGid
        LEFT JOIN Stock AS S 
            ON S.ArtGid = ART.ArtGid
        LEFT JOIN Recommended AS R 
            ON R.ArtGid = ART.ArtGid
        WHERE ISNULL(RCP.RcptGeblokkeerd, 0) = 0
        AND ISNULL(R.Recommended7Days, 0) > 0
        AND ISNULL(R.Recommended7Days, 0) > (ISNULL(S.TotalCurrentStockNL, 0) + ISNULL(S.TotalCurrentStockPL, 0))
        """;
        using var conn = await OpenAsync(ct);
        var rows = await conn.QueryAsync<Planning>(GetAllSqlNew);
        return rows.ToList();
    }
}
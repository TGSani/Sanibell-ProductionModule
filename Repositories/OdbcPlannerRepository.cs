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
        const string GetAllSql = """
                select     
                RcptOmschrijving    As ArticleDescription
                ,E.ArtCode           As ArticleNumber
                ,VRART_Maat          As Size
                ,VRART_Kleur         As Color

                ,Cast(isnull((Select  sum(ArtLocVoorraad)
                                    from 	KingSystem.tabArtikelLocatie
                                            inner join KingSystem.tabLocatie on LocGid = ArtLocLocGid
                                            inner join KingSystem.tabMagazijn on LocMagGid = MagGid
                                    Where 	MagCode in(1,2,9)
                                            and ArtLocArtGid = E.ArtGid
                                            and locnaam <> 'Retour voorraad' 
                            ),0)
                                As Decimal(5,0)) 								AS TotalCurrentStockNL

                    ,Cast(isnull((Select sum(ArtLocVoorraad)
                            from 	KingSystem.tabArtikelLocatie
                                    inner join KingSystem.tabLocatie on LocGid = ArtLocLocGid
                                        inner join KingSystem.tabMagazijn on LocMagGid = MagGid
                                Where 	MagCode in(5,8)
                                    and ArtLocArtGid = ArtGid
                                    and locnaam <> 'Retour voorraad' 
                            ),0) 			
                                As Decimal(5,0))								AS TotalCurrentStockPL

                ,Isnull((Select sum(OrrAantalBesteld - OrrAantalGeleverd) 
                    From   KingSystem.tabOrderRegel
                        Inner join KingSystem.tabOrderKop on OrrOrkGid = OrkGid
                    Where  OrkGoedgekeurd = 1 --Alleen goedgekeurde orders
                        And isnull(OrrLeverDatum,OrkLeverdatum) <= (getdate()+7)
                        And OrrArtGid = E.ArtGid             
                    ),0)                                As Recommended7Days

                ,Isnull((Select sum(OrrAantalBesteld - OrrAantalGeleverd) 
                    From   KingSystem.tabOrderRegel
                        Inner join KingSystem.tabOrderKop on OrrOrkGid = OrkGid
                    Where  OrkGoedgekeurd = 1 --Alleen goedgekeurde orders
                        And isnull(OrrLeverDatum,OrkLeverdatum) <= (getdate()+14)
                        And OrrArtGid = E.ArtGid             
                    ),0)                                As Recommended14Days

                ,Isnull((Select sum(OrrAantalBesteld - OrrAantalGeleverd) 
                    From   KingSystem.tabOrderRegel
                        Inner join KingSystem.tabOrderKop on OrrOrkGid = OrkGid
                    Where  OrkGoedgekeurd = 1 --Alleen goedgekeurde orders
                        And isnull(OrrLeverDatum,OrkLeverdatum) <= (getdate()+21)
                        And OrrArtGid = E.ArtGid             
                    ),0)                                As Recommended21Days


        From       KingSystem.tabReceptuur
                Inner join KingSystem.tabReceptuurEindproduct ON RcpeRcptGid = RcptGid
                Inner join KingSystem.tabArtikel E on E.Artgid = RcpeArtGid
                left join KingSystem.vrGetContent('ART',0,0,
                                                    'Maat
                                                    Kleur'
                                                    ,'','')
                                                WITH(VRART_Gid integer
                                                    ,VRART_Maat nchar(40)
                                                    ,VRART_Kleur nchar(40)
                                                    ) on VRART_gid = E.ArtGid

        Where      Isnull(RcptGeblokkeerd,0) = 0
                    AND  Recommended14Days > 1
        """;

        using var conn = await OpenAsync(ct);
        var rows = await conn.QueryAsync<Planning>(GetAllSql);
        return rows.ToList();
    }
}
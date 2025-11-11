using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Dapper;
using System.Data.Odbc;

namespace Sanibell_ProductionModule.Repositories;

public class OdbcOrderDetailRepository : IOrderDetailRepository
{
    private readonly string _cs;

    public OdbcOrderDetailRepository(IConfiguration cfg)
    {
        _cs = cfg.GetConnectionString("DemoArt")
            ?? throw new InvalidOperationException("Connectionstring ontbreekt");
    }

    private async Task<OdbcConnection> OpenAsync(CancellationToken ct)
    {
        var conn = new OdbcConnection(_cs);
        await conn.OpenAsync(ct);
        return conn;
    }

    private static readonly string GetByIdSql = """ 
        select   RcptCode AS ReceptCode
        ,RcpeRegelNr
        ,RcpeAantal AS Aantal
        ,ArtCode AS OrderNummer
        ,ArtOms
        ,VRART_Kleur 
        ,VRART_Maat
        ,ArtEANCode AS EANCode

        From      KingSystem.tabReceptuur
          inner join KingSystem.tabReceptuurEindproduct  ON RcpeRcptGid = RcptGid
          
          left join KingSystem.VrGetContent('RCP',0,0,'Opnemen in productie app','','')
                                        WITH(RCP_gid integer
                                            ,RCP_Opnemen integer
                                            ) RCP ON RCP_gid = RcptGid
          
          
           inner join KingSystem.tabArtikel on RcpeArtGid =  ArtGid
            left join KingSystem.VrGetContent('ART',0,0,
                                              'Kleur
                                               Maat
                                              ','','')
                                          WIth(VRART_gid integer
                                             ,VRART_Kleur nchar(20)
                                             ,VRART_Maat Nchar(20)
                                             ) VRART ON VRART_Gid = ArtGid
           left join KingSystem.tabArtikelEan on ArtGid = ArtEanArtGid
          
          
           
        /* voorbeeld om per etiket een regel te maken  */ 
        --   left join rowgenerator on row_num <= RcpeAantal
           
          
        Where       isnull(RCP_Opnemen,0) = 1
            AND ArtEanIsDefault = 1

        Order by     RcpeRegelNr
          
        """;

    public async Task<IReadOnlyList<OrderDetail>> GetDetailByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = await OpenAsync(ct);

        var rows = await conn.QueryAsync<OrderDetail>(GetByIdSql, new { id });
        return rows.ToList().AsReadOnly();
    }
}

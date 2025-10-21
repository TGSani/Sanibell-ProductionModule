using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Dapper;
using System.Data.Odbc;

namespace Sanibell_ProductionModule.Repositories;

public class OdbcOrderRepository : IOrderRepository
{

    // connection string
    private readonly string _cs;
    public OdbcOrderRepository(IConfiguration cfg)
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


    public async Task<Order?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        const string GetByIdSql = """
        SELECT   
        PrdOkNummer AS Id,
        PrdOkRcptCode AS RcptCode,
        PrdOkAantalRcpt AS Amount,
        PrdOkOmschrijving AS Note,
        CASE
            WHEN PrdOkStatus = '0' THEN 'Nieuw'
            WHEN PrdOkStatus = '1' THEN 'In te plannen'
            WHEN PrdOkStatus = '2' THEN 'Gereserveerd'
            WHEN PrdOkStatus = '3' THEN 'In productie' 
            WHEN PrdOkStatus = '4' THEN 'Afgerond'
            ELSE 'Onbekend'
        END AS Status,
        PrdOkTeProducerenVoor AS ProduceBefore,
        VRPRD_CreatedBy AS CreatedBy

        FROM KingSystem.tabProductieOrderKop
        LEFT JOIN KingSystem.vrGetContent('PRD',0,0,'CreatedBy','','')
            WITH(VRPRD_Gid integer,
            VRPRD_CreatedBy nchar(20)) 
            VRPRD ON VRPRD_Gid = PrdOkGid
        WHERE PrdOkNummer = ?
        """;

        using var conn = await OpenAsync(ct);
        var order = await conn.QuerySingleOrDefaultAsync<Order>(GetByIdSql, new { id });
        return order;
    }


    public async Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken ct = default)
    {
        const string GetAllSql = """
        SELECT   
        PrdOkNummer AS Id,
        PrdOkRcptCode AS RcptCode,
        PrdOkAantalRcpt AS Amount,
        PrdOkOmschrijving AS Note,
        CASE
            WHEN PrdOkStatus = '0' THEN 'Nieuw'
            WHEN PrdOkStatus = '1' THEN 'In te plannen'
            WHEN PrdOkStatus = '2' THEN 'Gereserveerd'
            WHEN PrdOkStatus = '3' THEN 'In productie' 
            WHEN PrdOkStatus = '4' THEN 'Afgerond'
            ELSE 'Onbekend'
        END AS Status,
        PrdOkTeProducerenVoor AS ProduceBefore,
        VRPRD_CreatedBy AS CreatedBy

        FROM KingSystem.tabProductieOrderKop
        LEFT JOIN KingSystem.vrGetContent('PRD',0,0,'CreatedBy','','')
            WITH(VRPRD_Gid integer,
                VRPRD_CreatedBy nchar(20)) 
                VRPRD ON VRPRD_Gid = PrdOkGid
            
            WHERE PrdOkStatus NOT IN ('0','1','4')
            ORDER BY PrdOkNummer
        """;


        using var conn = await OpenAsync(ct);
        var rows = await conn.QueryAsync<Order>(GetAllSql);
        return rows.AsList();
    }
}
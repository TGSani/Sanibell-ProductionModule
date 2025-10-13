using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Dapper;
using System.Data.Odbc;

namespace Sanibell_ProductionModule.Repositories;

public class OdbcProductionRepository : IProductionRepository
{

        // connection string
    private readonly string _cs;
    public OdbcProductionRepository(IConfiguration cfg)
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

    // LET OP!!! MOET NOG GELEVERD WORDEN!!
     private static readonly string GetAllSql = """ 

            """;


    public async Task<IReadOnlyList<Production>> GetProductionsAsync(CancellationToken ct = default)
    {
        using var conn = await OpenAsync(ct);
        var rows = await conn.QueryAsync<Production>(GetAllSql);
        return rows.AsList();
    }
}
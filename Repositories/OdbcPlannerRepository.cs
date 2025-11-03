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
        Binnenkort Aangeleverd
        """;

        using var conn = await OpenAsync(ct);
        var rows = await conn.QueryAsync<Planning>(GetAllSql);
        return rows.ToList();
    }
}
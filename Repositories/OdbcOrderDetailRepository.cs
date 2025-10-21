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
        SELECT 
            Id,
            OrderNumber,
            ProductName,
            Quantity,
            Status
        FROM OrderDetails
        WHERE Id = ?
        """;

    public async Task<IReadOnlyList<OrderDetail>> GetDetailByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = await OpenAsync(ct);

        var rows = await conn.QueryAsync<OrderDetail>(GetByIdSql, new { id });
        return rows.ToList().AsReadOnly();
    }
}

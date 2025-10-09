using System.Data.Odbc;
using Dapper;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public sealed class OdbcUserRepository : IUsersRepository
{
    // SQL to get all users
    private static readonly string GetAllSql = """
            SELECT  VRCP_Productie AS Role,
                    RelNummer AS Id,
                    RelVoornaam AS Name,
                    VRCP_WMS_Inlog AS QRcode

            From        KingSystem.tabRelatie 
                        Inner join KingSystem.tabNawFile on NawFilNawGid = RelNawGid
                        left join Kingsystem.vrGetContent('CP',3,3,
                                                        'Productie,
                                                        WMS Inlog'
                                                        
                                                        ,'','')
                                                    WITH(VRCP_RelGid integer
                                                        ,VRCP_Productie nchar(20)
                                                        ,VRCP_WMS_Inlog nchar(20)
                                                        ) VRCP on VRCP_Relgid = RelGid
            Where       NawFilFonId = 'B'
            AND VRCP_Productie IS NOT NULL
            AND VRCP_Productie <> 'NEE'
            """;

    // SQL to get user by id
    private static readonly string GetByIdSql = """
            SELECT  VRCP_Productie AS Role,
                    RelNummer AS Id,
                    RelVoornaam AS Name,
                    VRCP_WMS_Inlog AS QRcode

            From        KingSystem.tabRelatie 
                        Inner join KingSystem.tabNawFile on NawFilNawGid = RelNawGid
                        left join Kingsystem.vrGetContent('CP',3,3,
                                                        'Productie,
                                                        WMS Inlog'
                                                        
                                                        ,'','')
                                                    WITH(VRCP_RelGid integer
                                                        ,VRCP_Productie nchar(20)
                                                        ,VRCP_WMS_Inlog nchar(20)
                                                        ) VRCP on VRCP_Relgid = RelGid
            Where       NawFilFonId = 'B' 
            AND Relnummer = ?
            AND VRCP_Productie IS NOT NULL
            AND VRCP_Productie <> 'NEE'
            """;

    // connection string
    private readonly string _cs;
    public OdbcUserRepository(IConfiguration cfg)
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

    // get all users
    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = await OpenAsync(ct);
        var rows = await conn.QueryAsync<User>(GetAllSql);
        return rows.AsList();
    }

    // get user by id
    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = await OpenAsync(ct);
        var user = await conn.QueryFirstOrDefaultAsync<User>(GetByIdSql, new { id });
        return user;
    }
}
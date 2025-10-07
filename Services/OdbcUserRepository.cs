using System.Data.Odbc;
using Dapper;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Services;

public sealed class OdbcUserRepository : IUsersRepository
{
    // gets all users to generate tiles
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

    // GetById for users by id
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

    private readonly string _cs;
    public OdbcUserRepository(IConfiguration cfg)
    => _cs = cfg.GetConnectionString("DemoArt")
        ?? throw new InvalidOperationException("Connectionstring ontbreekt");

    private async Task<OdbcConnection> OpenAsync(CancellationToken ct)
    {
        var conn = new OdbcConnection(_cs);
        await conn.OpenAsync(ct);
        return conn;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
    {
        using var conn = await OpenAsync(ct);
        var rows = await conn.QueryAsync<User>(GetAllSql);
        return rows.AsList();
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var conn = await OpenAsync(ct);
        var user = await conn.QueryFirstOrDefaultAsync<User>(GetByIdSql, new { id });
        return user;
    }
}
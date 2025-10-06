using System.Data.Odbc;
using Microsoft.Extensions.Configuration;

namespace Sanibell_ProductionModule.Services
{
    public class DatabaseService
    {
        private readonly string? _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DemoArt");
        }

        public async Task<List<Dictionary<string, object?>>> ExecuteQueryAsync(string sql, params object[] parameters)
        {
            var result = new List<Dictionary<string, object?>>();

            using var connection = new OdbcConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new OdbcCommand(sql, connection);

            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue("?", param);
            }

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var colName = reader.GetName(i);
                    var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                    row[colName] = value;
                }

                result.Add(row);
            }

            return result;
        }
    }
}
using Sanibell_ProductionModule.Models;

public interface IDatabaseService
{
    Task<List<Dictionary<string, object>>> ExecuteQueryAsync(string sql, params object[] parameters);
}

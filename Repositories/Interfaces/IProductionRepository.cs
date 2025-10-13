using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Repositories.Interfaces;

public interface IProductionRepository
{
    Task<IReadOnlyList<Production>> GetProductionsAsync(CancellationToken ct = default);
}
using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Repositories.Interfaces;

public interface IPlannerRepository
{
    Task<IReadOnlyList<Planning>> GetPlanningAsync(CancellationToken ct = default);

    Task<Planning?> GetByIdAsync(int id, CancellationToken ct = default);
}
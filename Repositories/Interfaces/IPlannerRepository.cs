using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Repositories.Interfaces;

public interface IPlannerRepository
{
    Task<List<Planning>> GetPlanningAsync(CancellationToken ct = default);

    Task<List<Planning>> GetByIdAsync(int id, CancellationToken ct = default);
}
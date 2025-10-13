using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Repositories.Interfaces;

public interface IPlannerRepository
{
    Task<IReadOnlyList<Planning>> GetPlanningAsync(CancellationToken ct = default);

}
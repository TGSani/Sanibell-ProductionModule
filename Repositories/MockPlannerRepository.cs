using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockPlannerRepository : IPlannerRepository
{
    private static readonly IReadOnlyList<Planning> _plannings =
        [
        new Planning {}
    ];

    public Task<IReadOnlyList<Planning>> GetPlanningAsync(CancellationToken ct = default)
        => Task.FromResult(_plannings);
}
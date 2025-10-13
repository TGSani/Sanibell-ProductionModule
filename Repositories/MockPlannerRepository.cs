using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockPlannerRepository : IPlannerRepository
{
    private static readonly IReadOnlyList<Planning> _plannings =
        [
        new Planning {AdviesId = 1, EindProduct= "TestProduct1", ArtikelNummer = "TestNummer", ReceptCode = "TestCode", AanbevolenAantal = 6, MaximaalAantal = 20},
        new Planning {AdviesId = 2, EindProduct= "TestProduct2", ArtikelNummer = "TestNummer", ReceptCode = "TestCode", AanbevolenAantal = 2, MaximaalAantal = 20},
        new Planning {AdviesId = 3, EindProduct= "TestProduct3", ArtikelNummer = "TestNummer", ReceptCode = "TestCode", AanbevolenAantal = 1, MaximaalAantal = 20},
        new Planning {AdviesId = 4, EindProduct= "TestProduct4", ArtikelNummer = "TestNummer", ReceptCode = "TestCode", AanbevolenAantal = 20, MaximaalAantal = 18},
    ];

    public Task<IReadOnlyList<Planning>> GetPlanningAsync(CancellationToken ct = default)
        => Task.FromResult(_plannings);

        public Task<Planning?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var plannings = _plannings.FirstOrDefault(u => u.AdviesId == id);
        return Task.FromResult(plannings);
    }
}
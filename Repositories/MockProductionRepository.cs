using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockProductionRepository : IProductionRepository
{
    private static readonly IReadOnlyList<Production> _production =
        [
        new Production {Id = 1, Recept = "Wastafel, Interessante woorden", Date = DateTime.Parse("12-02-2015"), Aantal = 4, Urgent = true},

    ];

    public Task<IReadOnlyList<Production>> GetProductionsAsync(CancellationToken ct = default)
         => Task.FromResult(_production);

}    


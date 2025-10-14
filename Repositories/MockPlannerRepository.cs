using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Repositories;

public class MockPlannerRepository : IPlannerRepository
{
    private static readonly IReadOnlyList<Planning> _plannings =
    [
        new Planning { ArticleNumber = 14234, ArticleDescription = "Wasbak", Size = "254x275", Color = "Red", TotalCurrentStockNL = 200, TotalCurrentStockPL = 50, Recommended7Days = 7, Recommended14Days = 12, Recommended30Days = 40, MaxPossibleProduction = 200, ReadyDate = DateTime.Today.AddDays(5) },
        new Planning { ArticleNumber = 14235, ArticleDescription = "Kraan", Size = "Standaard", Color = "Chroom", TotalCurrentStockNL = 120, TotalCurrentStockPL = 80, Recommended7Days = 10, Recommended14Days = 16, Recommended30Days = 45, MaxPossibleProduction = 180, ReadyDate = DateTime.Today.AddDays(5) },
        new Planning { ArticleNumber = 14236, ArticleDescription = "Toiletpot", Size = "Normaal", Color = "Wit", TotalCurrentStockNL = 60, TotalCurrentStockPL = 40, Recommended7Days = 8, Recommended14Days = 14, Recommended30Days = 35, MaxPossibleProduction = 100, ReadyDate = DateTime.Today.AddDays(6) },
        new Planning { ArticleNumber = 14237, ArticleDescription = "Badkuip", Size = "160x70", Color = "Wit", TotalCurrentStockNL = 15, TotalCurrentStockPL = 10, Recommended7Days = 5, Recommended14Days = 8, Recommended30Days = 25, MaxPossibleProduction = 50, ReadyDate = DateTime.Today.AddDays(7) },
        new Planning { ArticleNumber = 14238, ArticleDescription = "Douchewand", Size = "90x200", Color = "Helder Glas", TotalCurrentStockNL = 30, TotalCurrentStockPL = 20, Recommended7Days = 6, Recommended14Days = 9, Recommended30Days = 28, MaxPossibleProduction = 70, ReadyDate = DateTime.Today.AddDays(5) },
        new Planning { ArticleNumber = 14239, ArticleDescription = "Spiegel", Size = "80x60", Color = "Zilver", TotalCurrentStockNL = 100, TotalCurrentStockPL = 60, Recommended7Days = 10, Recommended14Days = 15, Recommended30Days = 40, MaxPossibleProduction = 150, ReadyDate = DateTime.Today.AddDays(5) },
        new Planning { ArticleNumber = 14240, ArticleDescription = "Handdoekrek", Size = "50cm", Color = "Zwart", TotalCurrentStockNL = 90, TotalCurrentStockPL = 45, Recommended7Days = 9, Recommended14Days = 13, Recommended30Days = 30, MaxPossibleProduction = 100, ReadyDate = DateTime.Today.AddDays(8) },
        new Planning { ArticleNumber = 14241, ArticleDescription = "Zeepdispenser", Size = "Klein", Color = "Wit", TotalCurrentStockNL = 140, TotalCurrentStockPL = 70, Recommended7Days = 11, Recommended14Days = 18, Recommended30Days = 42, MaxPossibleProduction = 180, ReadyDate = DateTime.Today.AddDays(5) },
        new Planning { ArticleNumber = 14242, ArticleDescription = "Toiletrolhouder", Size = "Standaard", Color = "Chroom", TotalCurrentStockNL = 180, TotalCurrentStockPL = 90, Recommended7Days = 12, Recommended14Days = 20, Recommended30Days = 48, MaxPossibleProduction = 200, ReadyDate = DateTime.Today.AddDays(5) },
        new Planning { ArticleNumber = 14243, ArticleDescription = "Douchestang", Size = "70cm", Color = "Zilver", TotalCurrentStockNL = 80, TotalCurrentStockPL = 30, Recommended7Days = 8, Recommended14Days = 14, Recommended30Days = 36, MaxPossibleProduction = 100, ReadyDate = DateTime.Today.AddDays(6) },
    ];


    public Task<IReadOnlyList<Planning>> GetPlanningAsync(CancellationToken ct = default)
        => Task.FromResult(_plannings);

    public Task<Planning?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var plannings = _plannings.FirstOrDefault(u => u.ArticleNumber == id);
        return Task.FromResult(plannings);
    }
}
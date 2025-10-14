namespace Sanibell_ProductionModule.Models;

public sealed class Planning
{
    public required int ArticleNumber { get; init; }
    public required string ArticleDescription { get; init; }
    public required string Size { get; init; }
    public required string Color { get; init; }
    public required int TotalCurrentStockNL { get; init; }
    public required int TotalCurrentStockPL { get; init; }
    public required int Recommended7Days { get; init; }
    public required int Recommended14Days { get; init; }
    public required int Recommended30Days { get; init; }
    public required int MaxPossibleProduction { get; init; }
    public required DateTime ReadyDate { get; init; }
    public int? Amount { get; set; } // standaard = RecommendedAmount
    public bool Urgency { get; set; } = false;
    public bool Selection { get; set; } = false;
}
namespace Sanibell_ProductionModule.Models;

public class Planning
{
    public int ArticleNumber { get; init; }
    public string ArticleDescription { get; init; } = "";
    public string Size { get; init; } = "";
    public string Color { get; init; } = "";

    public int TotalCurrentStockNL { get; init; }
    public int TotalCurrentStockPL { get; init; }

    public int Recommended7Days { get; init; }
    public int Recommended14Days { get; init; }
    public int Recommended30Days { get; init; }

    public int MaxPossibleProduction { get; init; }
    public string ReadyDate { get; init; }
}

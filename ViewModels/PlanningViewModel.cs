namespace Sanibell_ProductionModule.ViewModels;

public class PlanningViewModel
{
    // readonly fields
    public int ArticleNumber { get; set; }
    public string ArticleDescription { get; set; } = "";
    public string Size { get; set; } = "";
    public string Color { get; set; } = "";
    public int TotalCurrentStockNL { get; set; }
    public int TotalCurrentStockPL { get; set; }
    public int Recommended7Days { get; set; }
    public int Recommended14Days { get; set; }
    public int Recommended30Days { get; set; }
    public int MaxPossibleProduction { get; set; }
    public string? ReadyDate { get; set; }

    // editable fields
    public int Amount { get; set; }
    public bool Urgency { get; set; }
    public bool Selection { get; set; }
}

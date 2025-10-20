namespace Sanibell_ProductionModule.Models;

//data voor de orders die in productie zijn of zijn geweest
public sealed class Order
{
    public required int Id { get; init; }
    public required string RcptCode { get; init; }
    public required string Status { get; init; }
    public required string Note { get; init; }
    public required int Amount { get; init; }
    public required string CreatedBy { get; init; }
    public required DateTime ProduceBefore { get; init; }
}
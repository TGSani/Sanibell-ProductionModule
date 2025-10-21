namespace Sanibell_ProductionModule.Models;

public sealed class OrderDetail
{
    public required int Id { get; init; }
    public required string Recept { get; set; }
    public required DateTime Date { get; set; }
    public required int Aantal { get; set; }
    public required bool Urgent { get; set; }
}
namespace Sanibell_ProductionModule.Models;

public sealed class OrderDetail
{
    public required int Id { get; set; }
    public required string Component { get; set; }
    public required int Aantal { get; set; }
    public required string Locatie { get; set; }
}
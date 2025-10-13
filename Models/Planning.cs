namespace Sanibell_ProductionModule.Models;

public sealed class Planning
{
    public required int AdviesId { get; init; }
    public required string EindProduct { get; init; }
    public required string ArtikelNummer { get; init; }
    public required string ReceptCode { get; init; }
    public required int AanbevolenAantal { get; init; }
    public required int MaximaalAantal { get; init; }
}
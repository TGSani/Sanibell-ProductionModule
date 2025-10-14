namespace Sanibell_ProductionModule.Models;

public sealed class Planning
{
    public required int AdviesId { get; init; }
    public required string ArtikelOmschrijving { get; init; }
    public required string ArtikelNummer { get; init; }
    public required string Soort { get; init; }
    public string? Status { get; set; } = "Nieuw advies";
    public required int AanbevolenAantal { get; init; }
    public required int HuidigAantal { get; init; }
    public required int MaximaalAantal { get; init; }
}
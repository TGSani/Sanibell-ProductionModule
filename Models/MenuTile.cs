namespace Sanibell_ProductionModule.Models;

// data voor de menu tegels op het dashboard
public sealed class MenuTile
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Icon { get; init; }
    public required string Href { get; init; }
    public required string[] Roles { get; init; }
}
namespace Sanibell_ProductionModule.Models;

// laat de minimale data zien die ik wil gebruiken om in te loggen
public sealed class User
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Role { get; init; }
    public string? QRcode { get; init; }
}
namespace Sanibell_ProductionModule.Models;


// laat de minimale data zien die ik wil gebruiken om in te loggen
public sealed class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    public string? QRcode { get; set; }
}
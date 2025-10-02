namespace Sanibell_ProductionModule.Models;


// laat de minimale data zien die ik wil gebruiken om in te loggen
public class Users
{
    public int Id { get; set; }
    public required string Naam { get; set; }
    public required string Rol { get; set; }
    public required string QRcode { get; set; }
}
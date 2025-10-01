namespace Sanibell_ProductionModule.Models;


// laat de minimale data zien die ik wil gebruiken
public class Users
{
    public int Id { get; set; }
    public required string Naam { get; set; }
    public required string Rol { get; set; }
}
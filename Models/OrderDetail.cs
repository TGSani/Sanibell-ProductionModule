namespace Sanibell_ProductionModule.Models;

public class OrderDetail
{
    public int OrderNummer { get; set; }
    public string ReceptCode { get; set; } = string.Empty;
    public string Omschrijving { get; set; } = string.Empty;
    public int Aantal { get; set; }
    public List<Component> Components { get; set; } = new();
}

public class Component
{
    public string Naam { get; set; } = string.Empty;
    public int Aantal { get; set; }
    public string LocatieCode { get; set; } = string.Empty;
}
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;
using System.Security.Claims;

public sealed class MenuTileService : IMenuTileService
{
    private static readonly MenuTile[] All =
    [
        new() { Title="Orders", Description="De totaallijst met te produceren productie orders", Icon="/icons/Orders.png", Href="/", Roles = ["Admin", "Planner", "User"] },
        new() { Title="Production", Description="Producties starten en sluiten", Icon="/icons/Productie.png", Href="/", Roles = ["Admin", "Planner", "User"] },
        new() { Title="Planning", Description="Productie advieslijst maken", Icon="/icons/Planning.png", Href="/", Roles = ["Admin", "Planner"] },
        new() { Title="Config", Description="Configuratie instellingen wijzigen", Icon="/icons/Settings.png", Href="/", Roles = ["Admin"] }
    ];


    public IEnumerable<MenuTile> GetTilesFor(ClaimsPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated != true)
            return All.Where(t => t.Roles.Length == 0); // public tiles only

        // if Roles empty, always show. otherwise base on user-roles
        var userRoles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToHashSet(StringComparer.OrdinalIgnoreCase);
        
        return All.Where(t => t.Roles.Length == 0 || t.Roles.Any(userRoles.Contains));
    }

}
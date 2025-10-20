using Sanibell_ProductionModule.Models;
using System.Security.Claims;

namespace Sanibell_ProductionModule.Services;

// service to get menu tiles based on user roles
public sealed class MenuTileService 
{
    // all tiles
    private static readonly MenuTile[] All =
    [
        new() { Title="Orders", Description="De totaallijst met te produceren productie orders", Icon="/icons/Orders.png", Href="/User/Order", Roles = ["Administrator", "Planner", "Productie medewerker"] },
        new() { Title="Production", Description="Producties starten en sluiten", Icon="/icons/Productie.png", Href="/User/Production", Roles = ["Administrator", "Planner", "Productie medewerker"] },
        new() { Title="Planning", Description="Productie advieslijst maken", Icon="/icons/Planning.png", Href="/Planner/PlannerAdvies", Roles = ["Administrator", "Planner"] },
        new() { Title="Config", Description="Configuratie instellingen wijzigen", Icon="/icons/Settings.png", Href="/Admin/Index", Roles = ["Administrator"] }
    ];


    // get tiles for user based on roles
    public IEnumerable<MenuTile> GetTilesFor(ClaimsPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated != true)
            return All.Where(t => t.Roles.Length == 0); 

        var userRoles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToHashSet(StringComparer.OrdinalIgnoreCase); 

        return All.Where(t => t.Roles.Length == 0 || t.Roles.Any(userRoles.Contains));
    }

}
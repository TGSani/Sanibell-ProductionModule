using System.Security.Claims;
using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Services.Interfaces;


public interface IMenuTileService
{
    IEnumerable<MenuTile> GetTilesFor(ClaimsPrincipal user);
}
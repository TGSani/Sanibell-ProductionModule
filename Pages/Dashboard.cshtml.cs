using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Sanibell_ProductionModule.Pages
{
    [Authorize (Policy = "RequireProductionRole")]
    public class DashboardModel : PageModel
    {
        private readonly MenuTileService _menuTileService;

        public IReadOnlyList<MenuTile> Tiles { get; private set; } = Array.Empty<MenuTile>();
        
        public DashboardModel(MenuTileService menuTileService)
        {
            _menuTileService = menuTileService;
        }
        public void OnGet()
        {
            // ViewData flags for displaying buttons in the layout
            ViewData["ShowBackButton"] = false;
            ViewData["ShowLogoutButton"] = true;

            //get all users to display on the index page
            Tiles = _menuTileService.GetTilesFor(User).ToList();
        }
    }
}

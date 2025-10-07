using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Pages
{
    public class DashboardModel(IMenuTileService menu) : PageModel
    {

        public IReadOnlyList<MenuTile> Tiles { get; private set; } = [];
        public void OnGet()
        {
            // ViewData flags for displaying buttons in the layout
            ViewData["ShowBackButton"] = false;
            ViewData["ShowLogoutButton"] = true;

            //get all users to display on the index page
            Tiles = menu.GetTilesFor(User).ToList();
        }

        public void OnPost()
        {
            
        }
    }
}

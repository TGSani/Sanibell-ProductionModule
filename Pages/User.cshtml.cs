using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sanibell_ProductionModule.Pages
{
    [Authorize(Roles = "User, Planner, Admin")]
    public class UserModel : PageModel
    {
        private readonly ILogger<UserModel> _logger;

        public UserModel(ILogger<UserModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Zet de ViewData flags voor het weergeven van de knoppen in de layout
            ViewData["ShowBackButton"] = false;
            ViewData["ShowLogoutButton"] = true;
        }
    }

}

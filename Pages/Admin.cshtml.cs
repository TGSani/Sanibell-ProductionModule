using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sanibell_ProductionModule.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private readonly ILogger<AdminModel> _logger;

        public AdminModel(ILogger<AdminModel> logger)
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
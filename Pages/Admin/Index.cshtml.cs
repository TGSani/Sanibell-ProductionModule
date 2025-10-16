using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sanibell_ProductionModule.Pages.Admin
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // ViewData flags for displaying buttons in the layout
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;
            ViewData["ReturnUrl"] = Url.Page("/Dashboard");
        }
    }

}
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Pages
{
    [Authorize(Policy = "RequirePlannerRole")]
    public class PlannerDetailModel : PageModel
    {
        private readonly IPlannerRepository _details;

        [BindProperty(SupportsGet = true)]
        public int AdviesId { get; set; }

        [BindProperty]
        public string Amount { get; set; } = string.Empty;

        [BindProperty]
        public bool Urgent { get; set; }

        public Models.Planning? Details { get; set; }

        public PlannerDetailModel(IPlannerRepository details)
        {
            _details = details;
        }

        // OnGet method to retrieve user by Id and display the login page
        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;

            if (AdviesId > 0)
            {
                Details = await _details.GetByIdAsync(AdviesId);
            }

            return Page();

        }
    }
}
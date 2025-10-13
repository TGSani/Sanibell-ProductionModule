using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Pages.Planner
{
    [Authorize(Policy = "RequirePlannerRole")]
    public class PlannerAdviesModel : PageModel
    {
        private readonly IPlannerRepository _planner;
        public PlannerAdviesModel(IPlannerRepository planner)
        {
            _planner = planner;
        }
        
        public IReadOnlyList<Planning>? Planners { get; private set; }

        public async Task OnGetAsync()
        {
            // Zet de ViewData flags voor het weergeven van de knoppen in de layout
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;

            Planners = await _planner.GetPlanningAsync();

        }
    }

}

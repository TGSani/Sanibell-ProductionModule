using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.ViewModels;


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

        [BindProperty]
        public List<PlanningViewModel> Planners { get; set; } = new();
    
        public async Task OnGetAsync()
        {
            // set ViewData flags for buttons in shared layout
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;
            ViewData["ReturnUrl"] = Url.Page("/Dashboard");

            var data = await _planner.GetPlanningAsync();

            Planners = data.Select(p => new PlanningViewModel
            {
                ArticleNumber = p.ArticleNumber,
                ArticleDescription = p.ArticleDescription,
                Size = p.Size,
                Color = p.Color,
                TotalCurrentStockNL = p.TotalCurrentStockNL,
                TotalCurrentStockPL = p.TotalCurrentStockPL,
                Recommended7Days = p.Recommended7Days,
                Recommended14Days = p.Recommended14Days,
                Recommended30Days = p.Recommended30Days,
                MaxPossibleProduction = p.MaxPossibleProduction,
                ReadyDate = p.ReadyDate,
                Amount = p.Recommended7Days, // default value
            }).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;

            // process selected orders
            var selectedOrders = Planners.Where(p => p.Selection).ToList();

            if (selectedOrders.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Selecteer minimaal één order om aan te maken.");
                return Page();
            }


            foreach (var order in selectedOrders)
            {
                // log info, sending back to ERP later
                Console.WriteLine($"Artikel {order.ArticleNumber}: Amount={order.Amount}, Urgency={order.Urgency}");
            }

            TempData["Message"] = $"{selectedOrders.Count} orders verwerkt.";
            return RedirectToPage();
        }
    }

}

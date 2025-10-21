using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Services;
using Sanibell_ProductionModule.ViewModels;


namespace Sanibell_ProductionModule.Pages.Planner
{
    [Authorize(Policy = "RequirePlannerRole")]
    public class PlannerAdviesModel : PageModel
    {
        private readonly IPlannerRepository _planner;
        private readonly PlannerErpService _erpService;
        public PlannerAdviesModel(IPlannerRepository planner, PlannerErpService erpService)
        {
            _planner = planner;
            _erpService = erpService;
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

            int successCount = 0;
            var failedOrders = new List<string>();

            foreach (var order in selectedOrders)
            {
                try
                {
                    var productieorderNummer = await _erpService.SendProductionOrderToErpAsync(order);

                    await _erpService.UnlockProductionOrderAsync(productieorderNummer);

                    var gebruiker = User.Identity?.Name ?? "Onbekend";
                    await _erpService.ProductionOrderCreatedByAsync(productieorderNummer, gebruiker);

                    await _erpService.UnlockProductionOrderAsync(productieorderNummer);

                    var Urgency = order.Urgency;
                    await _erpService.ProductionOrderUrgencyAsync(productieorderNummer, Urgency);

                    await _erpService.UnlockProductionOrderAsync(productieorderNummer);

                    successCount++;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"ERP fout bij order {order.ArticleNumber}: {ex.Message}");
                }

            }

            TempData["Message"] = $"{selectedOrders.Count} orders succesvol verzonden.";

            if (failedOrders.Any())
            {
                ModelState.AddModelError(string.Empty,
                    $"Fouten bij de volgende orders: {string.Join(", ", failedOrders)}");
            }

            return RedirectToPage();

        }
    }

}

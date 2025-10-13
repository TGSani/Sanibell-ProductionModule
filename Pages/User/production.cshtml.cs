using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Pages.User;

[Authorize (Policy = "RequireProductionRole")]
public class ProductionModel : PageModel
{
     private readonly IProductionRepository _production;

        public ProductionModel(IProductionRepository production)
        {
            _production = production;
        }

        public IReadOnlyList<Production>? Productions { get; private set; }
        public async Task OnGetAsync()
        {
            // Zet de ViewData flags voor het weergeven van de knoppen in de layout
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;

            Productions = await _production.GetProductionsAsync();


        }
}
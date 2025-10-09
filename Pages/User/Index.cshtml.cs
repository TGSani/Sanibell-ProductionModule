using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Pages.User
{
    [Authorize (Policy = "RequireProductionRole")]
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _orders;


        public IndexModel(IOrderRepository orders)
        {
            _orders = orders;
        }

        public IReadOnlyList<Models.Order>? Orders { get; private set; }
        public async Task OnGetAsync()
        {
            // Zet de ViewData flags voor het weergeven van de knoppen in de layout
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;

            Orders = await _orders.GetOrdersAsync();


        }
    }

}

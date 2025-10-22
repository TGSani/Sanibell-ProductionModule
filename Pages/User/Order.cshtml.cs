using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace Sanibell_ProductionModule.Pages.User
{
    [Authorize (Policy = "RequireProductionRole")]
    public class OrderModel : PageModel
    {
        private readonly IOrderRepository _orders;

        public OrderModel(IOrderRepository orders)
        {
            _orders = orders;
        }

        public IReadOnlyList<Order>? Orders { get; private set; }
        public async Task OnGetAsync()
        {
            // Zet de ViewData flags voor het weergeven van de knoppen in de layout
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;
            ViewData["ReturnUrl"] = Url.Page("/Dashboard");

            Orders = await _orders.GetOrdersAsync();
        }
    }

}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace Sanibell_ProductionModule.Pages.User;

[Authorize(Policy = "RequireProductionRole")]
public class OrderDetailModel : PageModel
{
    private readonly IOrderDetailRepository _detailRepo;

    public OrderDetailModel(IOrderDetailRepository detailRepo)
    {
        _detailRepo = detailRepo;
    }

    [BindProperty(SupportsGet = true)]
    public int OrderId { get; set; }

    public IReadOnlyList<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public async Task OnGetAsync()
    {
        ViewData["ShowBackButton"] = true;
        ViewData["ShowLogoutButton"] = false;
        ViewData["ReturnUrl"] = Url.Page("/User/Order");

        if (OrderId > 0)
        {
            OrderDetails = await _detailRepo.GetDetailByIdAsync(OrderId);
        }
    }
}

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
    private readonly IOrderRepository _orderRepo;
    public OrderDetailModel(IOrderDetailRepository detailRepo, IOrderRepository orderRepo)
    {
        _detailRepo = detailRepo;
        _orderRepo = orderRepo;
    }

    [BindProperty(SupportsGet = true)]
    public int OrderId { get; set; }

    public Order? Order { get; set; }
    public required IReadOnlyList<OrderDetail> Details { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["ShowBackButton"] = true;
        ViewData["ShowLogoutButton"] = false;
        ViewData["ReturnUrl"] = Url.Page("/User/Order");

        if (OrderId > 0)
        {
            Order = await _orderRepo.GetByIdAsync(OrderId);
            Details = await _detailRepo.GetDetailByIdAsync(OrderId);
        }
    }
}

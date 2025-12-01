using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services;
using Microsoft.AspNetCore.Mvc;
using Sanibell_ProductionModule.Services.Interfaces;



namespace Sanibell_ProductionModule.Pages.User;

[Authorize(Policy = "RequireProductionRole")]
public class OrderDetailModel : PageModel
{
    private readonly IOrderDetailRepository _detailRepo;
    private readonly IBarCodeGenService _barcodeService;
    private readonly IPlannerErpService _erpService;


    public OrderDetailModel(IOrderDetailRepository detailRepo, IBarCodeGenService barcodeService, IPlannerErpService erpService)
    {
        _detailRepo = detailRepo;
        _barcodeService = barcodeService;
        _erpService = erpService;
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
            var details = await _detailRepo.GetDetailByIdAsync(OrderId);

            // Maak een viewmodel met Base64 barcode
            OrderDetails = details.Select(d => new OrderDetail
            {
                OrderNummer = d.OrderNummer,
                ReceptCode = d.ReceptCode,
                Omschrijving = d.Omschrijving,
                Aantal = d.Aantal,
                EANCode = d.EANCode,
                Components = d.Components,
                BarcodeBase64 = _barcodeService.GenerateEan13Base64(d.EANCode)

            }).ToList().AsReadOnly();
        }
    }


    public async Task<IActionResult> OnPostStartOrderAsync()
    {
        ViewData["ShowBackButton"] = true;
        ViewData["ShowLogoutButton"] = false;

        await _erpService.ProductionOrderActiveStatusAsync(OrderId.ToString());
        await _erpService.UnlockProductionOrderAsync(OrderId.ToString());

        return RedirectToPage(new { this.OrderId });
    }

    public async Task<IActionResult> OnPostCompleteOrderAsync()
    {
        ViewData["ShowBackButton"] = true;
        ViewData["ShowLogoutButton"] = false;

        
        await _erpService.ProductionOrderVerwerkenAsync(OrderId.ToString());
        await _erpService.UnlockProductionOrderAsync(OrderId.ToString());

        return RedirectToPage(new { this.OrderId });
    }
}

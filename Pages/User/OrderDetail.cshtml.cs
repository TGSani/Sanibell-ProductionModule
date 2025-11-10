using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services;
using Microsoft.AspNetCore.Mvc;



namespace Sanibell_ProductionModule.Pages.User;

[Authorize(Policy = "RequireProductionRole")]
public class OrderDetailModel : PageModel
{
    private readonly IOrderDetailRepository _detailRepo;
    private readonly IBarCodeGenService _barcodeService;

    public OrderDetailModel(IOrderDetailRepository detailRepo, IBarCodeGenService barcodeService)
    {
        _detailRepo = detailRepo;
        _barcodeService = barcodeService;
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
}

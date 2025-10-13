using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sanibell_ProductionModule.Pages.User;

[Authorize (Policy = "RequireProductionRole")]
public class ProductionModel : PageModel
{

}
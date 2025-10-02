using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sanibell_ProductionModule.Pages
{
    public class PlannerModel : PageModel
    {
        private readonly ILogger<PlannerModel> _logger;

        public PlannerModel(ILogger<PlannerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}

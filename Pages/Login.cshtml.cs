using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Models;

namespace Sanibell_ProductionModule.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;


        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }
        public List<Users> Users { get; set; } = new();
        public void OnGet()
        {

        }
    }
}

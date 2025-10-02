using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUsersService _usersService;

        public IndexModel(ILogger<IndexModel> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }
        public List<Users> Users { get; set; } = new();
        public void OnGet()
        {
            // Zet de ViewData flags voor het weergeven van de knoppen in de layout
            ViewData["ShowBackButton"] = false;
            ViewData["ShowLogoutButton"] = false;

            // Haal alle gebruikers op bij het laden van de pagina
            Users = _usersService.GetAllUsers();
        }

        public void OnPost()
        {
            
        }
    }
}

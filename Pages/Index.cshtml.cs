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
            // ViewData flags for displaying buttons in the layout
            ViewData["ShowBackButton"] = false;
            ViewData["ShowLogoutButton"] = false;

            //get all users to display on the index page
            Users = _usersService.GetAllUsers();
        }

        public void OnPost()
        {
            
        }
    }
}

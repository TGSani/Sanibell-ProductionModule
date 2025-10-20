using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUsersRepository _users;
        public IndexModel(IUsersRepository users)
        {
          _users = users;  
        }
        public IReadOnlyList<Models.User> Users { get; set; } = Array.Empty<Models.User>();
        public async Task OnGetAsync(CancellationToken ct)
        {
            // ViewData flags for displaying buttons in the layout
            ViewData["ShowBackButton"] = false;
            ViewData["ShowLogoutButton"] = false;

            //get all users to display on the index page
            Users =  await _users.GetAllAsync(ct);
        }
    }
}

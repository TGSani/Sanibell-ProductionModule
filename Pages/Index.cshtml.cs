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
        public IReadOnlyList<Models.User>? Users { get; private set; }
        public async Task OnGetAsync()
        {
            // ViewData flags for displaying buttons in the layout
            ViewData["ShowBackButton"] = false;
            ViewData["ShowLogoutButton"] = false;

            //get all users to display on the index page
            Users =  await _users.GetAllAsync();
        }

        public void OnPost()
        {
            
        }
    }
}

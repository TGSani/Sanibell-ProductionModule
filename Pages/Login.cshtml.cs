using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IUsersService _usersService;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public string ScannedQRValue { get; set; } = string.Empty;

        public Users? Users { get; set; }

        public LoginModel(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // krijgt de userID binnen zodat alle data van de user later kan worden vergeleken uit de database
        public IActionResult OnGet()
        {
            Users = _usersService.GetById(Id);
            if (Users == null)
            {
                return NotFound();
            }

            return Page();
        }

        // vergelijkt de QR code met die uit de database en stuurt de user door naar de juiste pagina
        public IActionResult OnPost()
        {
            var users = _usersService.GetById(Id);
            if (users == null)
            {
                return NotFound();
            }

            if (users.QRcode == ScannedQRValue)
            {
                if (users.Rol == "Admin")
                {
                    return RedirectToPage("Admin");
                }
                else if (users.Rol == "Planner")
                {
                    return RedirectToPage("Planner");
                }
                else
                {
                    return RedirectToPage("User");
                }
            }

            ModelState.AddModelError(string.Empty, "QR-code is ongeldig.");
            Users = users;
            return Page();
        }
    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        // Zet de ViewData flags voor het weergeven van de knoppen in de layout
        private void SetHeaderButtons()
        {
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;
        }

        // krijgt de userID binnen zodat alle data van de user later kan worden vergeleken uit de database
        public IActionResult OnGet()
        {
            SetHeaderButtons();
            // Haal de gebruiker op op basis van de meegegeven ID
            Users = _usersService.GetById(Id);
            if (Users == null)
            {
                return NotFound();
            }

            return Page();
        }

        // vergelijkt de QR code met die uit de database en stuurt de user door naar de juiste pagina
        public async Task<IActionResult> OnPostAsync()
        {
            var users = _usersService.GetById(Id);

            if (users == null || users.QRcode != ScannedQRValue)
            {
                ModelState.AddModelError(string.Empty, "QR-code is ongeldig.");
                Users = users;

                SetHeaderButtons();
                return Page();
            }

            // Maak claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users.Name),
                new Claim(ClaimTypes.Role, users.Role)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            if (users.Role == "Admin")
            {
                return RedirectToPage("Admin");
            }
            else if (users.Role == "Planner")
            {
                return RedirectToPage("Planner");
            }
            else
            {
                return RedirectToPage("User");
            }



        }
    }
}

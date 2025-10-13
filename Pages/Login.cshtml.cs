using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Repositories.Interfaces;

namespace Sanibell_ProductionModule.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IUsersRepository _users;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public string ScannedQRValue { get; set; } = string.Empty;

        public Models.User? Users { get; set; }

        public LoginModel(IUsersRepository users)
        {
            _users = users;
        }
        // Set header buttons visibility
        private void SetHeaderButtons()
        {
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;
        }

        // OnGet method to retrieve user by Id and display the login page
        public async Task<IActionResult> OnGetAsync()
        {
            SetHeaderButtons();

            if (Id > 0)
            {
                Users = await _users.GetByIdAsync(Id);
            }

            return Page();

        }

        // compare the scanned QR code with the QR code from the database
        // if they match, create the claims and sign in the user
        public async Task<IActionResult> OnPostAsync()
        {
            SetHeaderButtons();

            Users = await _users.GetByIdAsync(Id);

            if (Users == null)
            {
                ModelState.AddModelError(string.Empty, "Gebruiker niet gevonden.");
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Users.QRcode) || Users.QRcode != ScannedQRValue)
                {
                    ModelState.AddModelError(string.Empty, "QR-code is ongeldig.");
                    return Page();
                }

            // Make claims
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, Users.Name),
                new Claim (ClaimTypes.Role, Users.Role)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal, new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = false
            });

            return RedirectToPage("/Dashboard");
        }
    }
}

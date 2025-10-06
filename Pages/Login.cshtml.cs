using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sanibell_ProductionModule.Models;
using Sanibell_ProductionModule.Services.Interfaces;

namespace Sanibell_ProductionModule.Pages
{
    [AllowAnonymous]
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
        // Set header buttons visibility
        private void SetHeaderButtons()
        {
            ViewData["ShowBackButton"] = true;
            ViewData["ShowLogoutButton"] = false;
        }

        // OnGet method to retrieve user by Id and display the login page
        public IActionResult OnGet()
        {
            SetHeaderButtons();

            Users = _usersService.GetById(Id);
            if (Users == null)
            {
                return NotFound();
            }

            return Page();
        }

        // compare the scanned QR code with the QR code from the database
        // if they match, create the claims and sign in the user
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

            // Make claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users.Name),
                new Claim(ClaimTypes.Role, users.Role)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal, new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = false
            });

            return users.Role switch
            {
                "Admin" => RedirectToPage("/Admin/index"),
                "Planner" => RedirectToPage("/Planner/index"),
                _ => RedirectToPage("/User/index")
            };
        }
    }
}

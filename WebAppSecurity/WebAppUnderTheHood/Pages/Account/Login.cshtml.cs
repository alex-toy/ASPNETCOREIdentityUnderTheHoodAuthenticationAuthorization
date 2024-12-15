using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebAppUnderTheHood.AuthenticationModel;

namespace WebAppUnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        private const string CookieAuth = "MyCookieAuth";

        [BindProperty]
        public Credential Credential { get; set; } = new ();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            bool areCredentialsOk = (new string[] {"alex", "seb", "bob", "john"}).Contains(Credential.Name) && Credential.Password == "123";
            if (!areCredentialsOk) return Page();

            List<Claim> claims = GetClaims(Credential.Name);

            ClaimsIdentity identity = new(claims, CookieAuth);
            ClaimsPrincipal principal = new(identity);

            AuthenticationProperties authProperties = new ()
            {
                IsPersistent = Credential.RememberMe,
            };

            await HttpContext.SignInAsync(CookieAuth, principal, authProperties);

            return RedirectToPage("/index");
        }

        private static List<Claim> GetClaims(string name)
        {
            List<Claim> claims =  new()
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "alex@test.fr"),
            };

            if (name == "alex") claims.Add(new Claim("Role", "Admin"));
            if (name == "seb") claims.Add(new Claim("Department", "HR"));
            if (name == "bob") claims.AddRange(new List<Claim>() {
                new Claim("Department", "HR"),
                new Claim("Role", "Admin"),
                new Claim("EmploymentDate", "2024-11-01")
            });
            if (name == "john") claims.AddRange(new List<Claim>() {
                new Claim("Department", "HR"),
                new Claim("Role", "Admin"),
                new Claim("EmploymentDate", "2024-02-01")
            });

            return claims;
        }
    }
}

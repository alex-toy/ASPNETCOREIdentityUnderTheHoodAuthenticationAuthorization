using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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

            bool areCredentialsOk = (Credential.Name == "alex" || Credential.Name == "seb") && Credential.Password == "123";
            if (!areCredentialsOk) return Page();

            List<Claim> claims = getClaims(Credential.Name);

            ClaimsIdentity identity = new(claims, CookieAuth);
            ClaimsPrincipal principal = new(identity);

            await HttpContext.SignInAsync(CookieAuth, principal);

            return RedirectToPage("/index");
        }

        private static List<Claim> getClaims(string name)
        {
            List<Claim> claims =  new()
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "alex@test.fr"),
            };

            if (name == "alex") claims.Add(new Claim("Role", "Admin"));
            if (name == "seb") claims.Add(new Claim("Department", "HR"));

            return claims;
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}

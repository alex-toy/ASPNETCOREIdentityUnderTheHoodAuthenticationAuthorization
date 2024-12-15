using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppUnderTheHood.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private const string CookieAuth = "MyCookieAuth";

        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync(CookieAuth);
            return RedirectToPage("/Index");
        }
    }
}

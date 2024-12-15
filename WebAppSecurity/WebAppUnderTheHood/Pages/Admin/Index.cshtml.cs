using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppUnderTheHood.Pages.Admin
{
    [Authorize(Policy = "MustBeAdmin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

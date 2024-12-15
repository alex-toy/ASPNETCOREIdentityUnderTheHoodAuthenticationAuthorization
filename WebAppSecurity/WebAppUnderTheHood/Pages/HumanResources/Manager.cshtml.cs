using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppUnderTheHood.Pages.HumanResources
{
    [Authorize(Policy = "MustBeHRManager")]
    public class ManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Data.Models;
using System.Threading.Tasks;

namespace MyBlog.Areas.Identity.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public IndexModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Articles");
        }
    }
}

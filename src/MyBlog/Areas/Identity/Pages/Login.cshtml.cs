using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Constants;
using MyBlog.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Areas.Identity.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public LoginModel(SignInManager<User> signInManger)
        {
            this.signInManager = signInManger;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public async Task<IActionResult> OnPostAsync()
        {
            if (await DoesUserWithUsernameExist())
            {
                var user = await signInManager.UserManager.FindByNameAsync(Username);

                var signInResult = await signInManager.CheckPasswordSignInAsync(user, Password, false);

                if (!signInResult.Succeeded)
                {
                    Errors.Add(WebConstants.LoginPasswordNotCorrect);
                }
                else
                {
                    await signInManager.SignInAsync(user, RememberMe);
                }
            }
            else
            {
                Errors.Add(WebConstants.UserWithUsernameDoesNotExist);
            }

            if (Errors.Any())
            {
                return Page();
            }

            return RedirectToAction("Index", "Articles");
        }

        private async Task<bool> DoesUserWithUsernameExist()
            => await signInManager.UserManager.FindByNameAsync(Username) != null;
    }
}

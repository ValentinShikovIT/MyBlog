using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Constants;
using MyBlog.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MyBlog.Areas.Identity.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public RegisterModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Required]
        [EmailAddress]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        [BindProperty]
        public string Username { get; set; }

        [Required]
        [BindProperty]
        public string Password { get; set; }

        [Required]
        [BindProperty]
        public string ComfirmPassword { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public async Task<IActionResult> OnPostAsync()
        {
            await ValidateBasics();

            var user = CreateUser();

            await ValidatePassword(user);

            if (!Errors.Any())
            {
                var isCreated = await CreateUserInAspIdentity(user);
                if (isCreated)
                {
                    await AddUserToRegularRole(user);
                }

                return RedirectToAction("Index", "Articles");
            }

            return Page();
        }

        private async Task ValidateBasics()
        {
            if (!ModelState.IsValid)
            {
                Errors
                    .AddRange(
                    ModelState.Values
                    .SelectMany(
                        x => x.Errors
                        .Select(x => x.ErrorMessage)
                        .ToList()));
            }

            if (!DoPasswordsMatch())
            {
                Errors.Add(WebConstants.PasswordAndComfirmDoNotMatch);
            }

            if (await DoesUserWithUsernameExist())
            {
                Errors.Add(WebConstants.UserWithUsernameAlreadyExists);
            }
        }

        private bool DoPasswordsMatch()
            => Password.Equals(ComfirmPassword, StringComparison.InvariantCulture);

        private async Task<bool> DoesUserWithUsernameExist()
            => await signInManager
            .UserManager
            .FindByEmailAsync(Email) != null;

        private async Task<bool> CreateUserInAspIdentity(User user)
        {
            var result = await signInManager
                .UserManager
                .CreateAsync(user, Password);

            return result.Succeeded;
        }

        private async Task AddUserToRegularRole(User user)
            => await signInManager
            .UserManager
            .AddToRoleAsync(user, WebConstants.RegularRoleName);

        private async Task ValidatePassword(User user)
        {
            foreach (var passwordValidator in signInManager.UserManager.PasswordValidators)
            {
                var validationResult = await passwordValidator
                    .ValidateAsync(signInManager.UserManager, user, Password);

                Errors
                    .AddRange(
                    validationResult
                    .Errors
                    .Select(e => e.Description));
            }
        }

        private User CreateUser()
            => new User
            {
                Email = Email,
                UserName = Username,
                SecurityStamp = WebConstants.Regular_SecurityStamp
            };
    }
}

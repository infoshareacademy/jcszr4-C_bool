using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C_bool.WebApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<User> _userRepository;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRepository<User> userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Photo { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set; }

            public Gender Gender { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var email = user.Email;
            var gender = user.Gender;
            var photo = user.Photo;
            var points = user.Points;
            var isActive = user.IsActive;
            var createdOn = user.CreatedOn;

            Username = userName;
            Email = email;
            Gender = gender;
            Photo = photo;
            Points = points;
            IsActive = isActive;
            CreatedOn = createdOn;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nie znaleziono użytkownika o ID: '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nie znaleziono użytkownika o ID: '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userName = user.UserName;
            if (Input.Username != userName)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "Coś poszło nie tak, spróbuj podać inną nazwę.";
                    return RedirectToPage();
                }
            }

            var userGender = user.Gender;
            if (Input.Gender != userGender)
            {
                user.Gender = Input.Gender;
                _userRepository.Update(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Twój profil został uaktualniony pomyślnie.";
            return RedirectToPage();
        }
    }
}

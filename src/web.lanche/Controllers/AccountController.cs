using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web.lanche.ViewModels;

namespace web.lanche.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel(){
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel){

            if (!ModelState.IsValid)
                return View(viewModel);

            var user = await _userManager.FindByNameAsync(viewModel.UserName);

            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(viewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index","Home");
                    }

                    return Redirect(viewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Falha ao realizar o login!");
            return View(viewModel);
        }
    }
}
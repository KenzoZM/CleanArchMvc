using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authentication;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAuthenticate authentication, ILogger<AccountController> logger)
        {
            _authentication = authentication;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authentication.Authenticate(model.Email, model.Password);

            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authentication.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong) " +
                    "password must have 1 upper char, 1 lower char, numbers and a special character.");
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsAdminTester()
        {
            var adminEmail = "admin@localhost";
            var adminPassword = "Numsey#2021"; 

            var result = await _authentication.Authenticate(adminEmail, adminPassword);
            if (result)
            {
                _logger.LogInformation("User logged in as admin tester."); 

               
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                _logger.LogError("Failed to log in as admin tester.");

                return RedirectToAction(nameof(Login));
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();
            return Redirect("/Account/Login");
        }
    }
}

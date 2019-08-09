using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cali_Remote_Pc.DTO;
using Cali_Remote_Pc.Entity;
using Cali_Remote_Pc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cali_Remote_Pc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        //private IClientRepository _clientRepository;
        private UserManager<User> _userManager;
        private SignInManager<User> _signManager;

        public UserController(IUserRepository userRepository/*, IClientRepository clientRepository*/, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            //_clientRepository = clientRepository;
            _userManager = userManager;
            _signManager = signInManager;
        }

        #region Login

        [HttpGet("/login")]
        public IActionResult Login(string returnUrl)
        {
            if (_signManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Default");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Username, model.Password, true, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Default");
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(model);
        }

        #endregion

        #region Register
        [HttpGet("/register")]
        public IActionResult Register()
        {
            if (_signManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }


        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        #endregion

        #region Logout
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signManager.SignOutAsync();
                return RedirectToAction("Login","User");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Default");
            }


        }
        #endregion
    }
}
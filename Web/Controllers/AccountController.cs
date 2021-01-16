using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.ViewModels;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userList = await _userRepository.GetAllUsers();
                var user = userList.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация
                    if (user.Role.Name == "admin")
                    {
                        return RedirectToAction("Product", "Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userList = await _userRepository.GetAllUsers();
                var user = userList.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Gender = model.Gender,
                        Email = model.Email,
                        Password = model.Password,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    var roleList = await _userRepository.GetAllRoles();
                    var userRole = roleList.FirstOrDefault(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    await _userRepository.AddUser(user);
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
    }
}

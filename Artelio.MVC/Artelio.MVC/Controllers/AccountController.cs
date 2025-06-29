using System.Security.Claims;
using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.Entities;
using Artelio.MVC.Exceptions.Auth;
using Artelio.MVC.Exceptions.Common;
using Artelio.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Artelio.MVC.Controllers
{
    public class AccountController(IAuthService _authService, UserManager<AppUser> _userManager, IProfileSettingService _profileService) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) return View(dto);
            AppUser user = null;

            try
            {
                user = await _authService.CheckUser(dto);
            }
            catch (SomethingIsWrongException)
            {
                ModelState.AddModelError("", "Username or Password is wrong.");
            }
            catch (EmailNotConfirmedException)
            {
                ModelState.AddModelError("", "Email not confirmed.");
            }

            if (!ModelState.IsValid) return View(dto);

            SignInResult result = await _authService.LoginAsync(user, dto.Password, dto.RememberMe);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "You try more time. In that case, you must wait for " + user.LockoutEnd.Value.ToString("HH:mm:ss"));
                return View(dto);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is wrong.");
                return View(dto);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) return View(dto);

            AppUser user = null;

            try
            {
                user = await _authService.RegisterAsync(dto);

                await _profileService.CreateSocialMediaAsync(user.Id);


            } catch (SomethingIsWrongException msg)
            {
                ModelState.AddModelError("", msg.Message);
            }


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var ConfirmationLink = Url.Action(nameof(ConfirmeEmail), "Account", new { token, Email = user.Email }, Request.Scheme);

            await _authService.Send(user.Email, "Confirmation link", ConfirmationLink);

            return RedirectToAction("Login");
        }


        public async Task<IActionResult> ConfirmeEmail(string token, string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            await _authService.ConfirmeEmail(token, email);

            return RedirectToAction("login");
        }

        [HttpGet]
        public async Task<IActionResult> FogotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> FogotPassword(GetEmailDTO dto)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                ModelState.AddModelError("", "Email is wrong or email is not confirmed.");
                return View(dto);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { token, email = dto.Email }, Request.Scheme);

            await _authService.Send(dto.Email, "Reset Password", callbackUrl);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string token, string email, ResetPasswordDTO dto)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Əgər login olubsa, əsas səhifəyə yönləndir
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) return View(dto);

            await _authService.ResetPassword(token, email, dto.NewPassword);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _authService.LogOut();

            return RedirectToAction("Login", "Account");
        }


    }
}

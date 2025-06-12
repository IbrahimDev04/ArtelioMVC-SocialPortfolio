using System.Net;
using System.Net.Mail;
using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.Entities;
using Artelio.MVC.Exceptions.Auth;
using Artelio.MVC.Exceptions.Common;
using Artelio.MVC.Services.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Artelio.MVC.Services.Implements
{
    public class AuthService(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IConfiguration _configuration) : IAuthService
    {
        public async Task<AppUser> CheckUser(LoginDTO dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
                if (user == null)
                    throw new SomethingIsWrongException("Username or password is wrong");
            }


            if (!await _userManager.IsEmailConfirmedAsync(user))
                throw new EmailNotConfirmedException("Email not confirmed.");

            return user;
        }

        public async Task ConfirmeEmail(string token, string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new Exception();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded) throw new Exception();
        }

        public async Task<GetProfileUserAboutDTO> GetProfileUserAbout(string UserId)
        {
            AppUser data = await _userManager.FindByIdAsync(UserId);

            GetProfileUserAboutDTO dto = new GetProfileUserAboutDTO
            {
                Company = data.Company,
                Job = data.Job,
                About = data.About,
                Country = data.Country,
                City = data.City,
                WebUrl = data.WebUrl
            };

            return dto;
        }

        public async Task<GetUserInfoDTO> GetUserInfo(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            GetUserInfoDTO dto = new GetUserInfoDTO
            {
                Id = userId,
                FullName = user.Name + " " + user.Surname,
                UserName = "@" + user.Name.ToLower() + user.Surname.ToLower(),
                ImageUrl = user.ImageUrl
            };

            return dto;
        }

        public async Task<GetUserProfileDTO> GetUserProfile(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            GetUserProfileDTO dto = new GetUserProfileDTO
            {
                Id = userId,
                FullName = user.Name + " " + user.Surname,
                Username = "@" + user.UserName.ToLower(),
                ImageUrl = user.ImageUrl,
                BannerUrl = user.BannerUrl.Substring(19)
            };

            return dto;
        }

        public async Task<SignInResult> LoginAsync(AppUser user, string Password, bool RememberMe)
        {
            await _signInManager.CheckPasswordSignInAsync(user, Password, true);
            var result = await _signInManager.PasswordSignInAsync(user, Password, RememberMe, true);

            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AppUser> RegisterAsync(RegisterDTO dto)
        {

            AppUser user = new AppUser
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                UserName = dto.Username,
                ImageUrl = "assets/imgs/Profile/download.png",
                BannerUrl = "assets/imgs/Banner/01.jpg"
            };

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new SomethingIsWrongException(error.Description);
                }
            }

            return user;
        }

        public async Task ResetPassword(string token, string email, string password)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new Exception();

            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (!result.Succeeded) throw new Exception();
        }

        public async Task Send(string mailTo, string subject, string body, bool isBodyHtml = false)
        {
            SmtpClient smtpClient = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));

            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Password"]);

            MailAddress from = new MailAddress(_configuration["Email:Login"], "Artelio");
            MailAddress to = new MailAddress(mailTo);

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isBodyHtml;

            smtpClient.Send(message);
        }
    }
}

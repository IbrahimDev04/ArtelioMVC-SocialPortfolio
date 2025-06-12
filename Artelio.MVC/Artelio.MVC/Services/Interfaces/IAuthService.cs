using Artelio.MVC.DTOs.Auth;
using Artelio.MVC.Entities;
using Microsoft.AspNetCore.Identity;

namespace Artelio.MVC.Services.Interfaces;

public interface IAuthService
{
    Task<AppUser> CheckUser(LoginDTO dto);
    Task<SignInResult> LoginAsync(AppUser user, string Password, bool RememberMe);
    Task<AppUser> RegisterAsync(RegisterDTO dto);
    Task Send(string mailTo, string subject, string body, bool isBodyHtml = false);
    Task ConfirmeEmail(string token, string email);
    Task ResetPassword(string token, string email, string password);
    Task<GetUserInfoDTO> GetUserInfo(string userId);
    Task<GetUserProfileDTO> GetUserProfile(string userId);
    Task<GetProfileUserAboutDTO> GetProfileUserAbout(string UserId);
    Task LogOut();



}

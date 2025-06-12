using System.ComponentModel.DataAnnotations;

namespace Artelio.MVC.DTOs.Auth;

public class LoginDTO
{
    public string UserNameOrEmail { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

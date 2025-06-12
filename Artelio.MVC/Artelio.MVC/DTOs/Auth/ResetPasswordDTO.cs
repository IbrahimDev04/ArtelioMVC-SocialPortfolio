using System.ComponentModel.DataAnnotations;

namespace Artelio.MVC.DTOs.Auth;

public class ResetPasswordDTO
{
    [Required, DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required, DataType(DataType.Password), Compare(nameof(NewPassword))]
    public string RepeatPassword { get; set; }
}

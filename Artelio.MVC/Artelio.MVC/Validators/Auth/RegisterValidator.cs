using Artelio.MVC.DTOs.Auth;
using FluentValidation;

namespace Artelio.MVC.Validators.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage($"Name can not be more than 50 characters");

            RuleFor(u => u.Surname)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage($"Name can not be more than 50 characters");

            RuleFor(u => u.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage($"Name can not be more than 50 characters");

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email is required")
                .MaximumLength(320)
                .WithMessage($"Email can not be more than 320 characters")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Invalid email format");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be longer than 8 characters")
                .MaximumLength(32)
                .WithMessage($"Password must be less than 32 characters");

            RuleFor(u => u.RepeatPassword)
                .Must((model, repeatPassword) => repeatPassword != model.Password)
                .WithMessage("Repeat password is not same with password");

        }
    }
}

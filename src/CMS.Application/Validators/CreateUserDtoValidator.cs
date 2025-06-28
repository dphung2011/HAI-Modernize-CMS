using CMS.Application.DTOs;
using FluentValidation;

namespace CMS.Application.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 20).WithMessage("Username must be between 3 and 20 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.EmailId)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailId))
            .WithMessage("A valid email address is required");

        RuleFor(x => x.Phone)
            .Matches(@"^\d{10}$").When(x => !string.IsNullOrEmpty(x.Phone))
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Role)
            .Must(role => string.IsNullOrEmpty(role) || 
                          role == "Admin" || 
                          role == "User" || 
                          role == "Faculty")
            .WithMessage("Role must be Admin, User, or Faculty");
    }
}
using CMS.Application.DTOs;
using FluentValidation;

namespace CMS.Application.Validators;

public class CreateFacultyDtoValidator : AbstractValidator<CreateFacultyDto>
{
    public CreateFacultyDtoValidator()
    {
        RuleFor(x => x.FacultyFirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(45).WithMessage("First name cannot exceed 45 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("First name can only contain letters and spaces");

        RuleFor(x => x.FacultyLastName)
            .MaximumLength(45).WithMessage("Last name cannot exceed 45 characters")
            .Matches(@"^[a-zA-Z\s]+$").When(x => !string.IsNullOrEmpty(x.FacultyLastName))
            .WithMessage("Last name can only contain letters and spaces");

        RuleFor(x => x.Gender)
            .Must(gender => string.IsNullOrEmpty(gender) || 
                            gender == "Male" || 
                            gender == "Female" || 
                            gender == "Other")
            .WithMessage("Gender must be Male, Female, or Other");

        RuleFor(x => x.Age)
            .InclusiveBetween(18, 100).When(x => x.Age.HasValue)
            .WithMessage("Age must be between 18 and 100");

        RuleFor(x => x.ContactNumber)
            .Matches(@"^\d{10}$").When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Contact number must be 10 digits");

        RuleFor(x => x.EmailId)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailId))
            .WithMessage("A valid email address is required");

        RuleFor(x => x.Pincode)
            .InclusiveBetween(100000, 999999).When(x => x.Pincode.HasValue)
            .WithMessage("PIN code must be a 6-digit number");

        RuleFor(x => x.Password)
            .MinimumLength(6).When(x => !string.IsNullOrEmpty(x.Password))
            .WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.Experience)
            .Matches(@"^\d{1,2}$").When(x => !string.IsNullOrEmpty(x.Experience))
            .WithMessage("Experience must be a numeric value less than 100");
    }
}
using CMS.Application.DTOs;
using FluentValidation;

namespace CMS.Application.Validators;

public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(20).WithMessage("First name cannot exceed 20 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("First name can only contain letters and spaces");

        RuleFor(x => x.LastName)
            .MaximumLength(30).WithMessage("Last name cannot exceed 30 characters")
            .Matches(@"^[a-zA-Z\s]+$").When(x => !string.IsNullOrEmpty(x.LastName))
            .WithMessage("Last name can only contain letters and spaces");

        RuleFor(x => x.Gender)
            .Must(gender => string.IsNullOrEmpty(gender) || 
                            gender == "Male" || 
                            gender == "Female" || 
                            gender == "Other")
            .WithMessage("Gender must be Male, Female, or Other");

        RuleFor(x => x.Age)
            .InclusiveBetween(1, 100).When(x => x.Age.HasValue)
            .WithMessage("Age must be between 1 and 100");

        RuleFor(x => x.ContactNumber)
            .Matches(@"^\d{10}$").When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Contact number must be 10 digits");

        RuleFor(x => x.EmailId)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailId))
            .WithMessage("A valid email address is required");

        RuleFor(x => x.Pincode)
            .InclusiveBetween(100000, 999999).When(x => x.Pincode.HasValue)
            .WithMessage("PIN code must be a 6-digit number");

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("Course name is required");
    }
}
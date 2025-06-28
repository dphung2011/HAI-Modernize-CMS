using CMS.Application.DTOs;
using FluentValidation;

namespace CMS.Application.Validators;

public class CreateAttendanceDtoValidator : AbstractValidator<CreateAttendanceDto>
{
    public CreateAttendanceDtoValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required")
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(30).WithMessage("Course name cannot exceed 30 characters");

        RuleFor(x => x.SubjectName)
            .NotEmpty().WithMessage("Subject name is required")
            .MaximumLength(30).WithMessage("Subject name cannot exceed 30 characters");

        RuleFor(x => x.SemOrYear)
            .NotEmpty().WithMessage("Semester/Year is required")
            .MaximumLength(30).WithMessage("Semester/Year cannot exceed 30 characters");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required")
            .MaximumLength(40).WithMessage("Date cannot exceed 40 characters")
            .Matches(@"^\d{4}-\d{2}-\d{2}$").When(x => !string.IsNullOrEmpty(x.Date))
            .WithMessage("Date must be in yyyy-MM-dd format");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(45).WithMessage("First name cannot exceed 45 characters");

        RuleFor(x => x.LastName)
            .MaximumLength(45).WithMessage("Last name cannot exceed 45 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .MaximumLength(30).WithMessage("Status cannot exceed 30 characters")
            .Must(status => status == "Present" || status == "Absent" || status == "Late")
            .WithMessage("Status must be Present, Absent, or Late");
    }
}
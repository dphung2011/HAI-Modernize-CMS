using CMS.Application.DTOs;
using FluentValidation;

namespace CMS.Application.Validators;

public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseDtoValidator()
    {
        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(30).WithMessage("Course name cannot exceed 30 characters");

        RuleFor(x => x.SemOrYear)
            .NotEmpty().WithMessage("Semester/Year type is required")
            .MaximumLength(20).WithMessage("Semester/Year type cannot exceed 20 characters");

        RuleFor(x => x.TotalSemOrYear)
            .NotNull().WithMessage("Total semesters/years is required")
            .InclusiveBetween(1, 12).WithMessage("Total semesters/years must be between 1 and 12");
    }
}
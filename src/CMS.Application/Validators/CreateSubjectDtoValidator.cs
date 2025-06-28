using CMS.Application.DTOs;
using FluentValidation;

namespace CMS.Application.Validators;

public class CreateSubjectDtoValidator : AbstractValidator<CreateSubjectDto>
{
    public CreateSubjectDtoValidator()
    {
        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(45).WithMessage("Course name cannot exceed 45 characters");

        RuleFor(x => x.SubjectName)
            .NotEmpty().WithMessage("Subject name is required")
            .MaximumLength(45).WithMessage("Subject name cannot exceed 45 characters")
            .Matches(@"^[a-zA-Z0-9\s\-]+$").WithMessage("Subject name can only contain letters, numbers, spaces, and hyphens");

        RuleFor(x => x.SemOrYear)
            .NotEmpty().WithMessage("Semester/Year is required")
            .MaximumLength(30).WithMessage("Semester/Year cannot exceed 30 characters");

        RuleFor(x => x.SubjectType)
            .MaximumLength(20).WithMessage("Subject type cannot exceed 20 characters")
            .Must(type => string.IsNullOrEmpty(type) ||
                          type == "Theory" ||
                          type == "Practical" ||
                          type == "Both")
            .WithMessage("Subject type must be Theory, Practical, or Both");

        RuleFor(x => x.TheoryMarks)
            .InclusiveBetween(0, 100).When(x => x.TheoryMarks.HasValue)
            .WithMessage("Theory marks must be between 0 and 100");

        RuleFor(x => x.PracticalMarks)
            .InclusiveBetween(0, 100).When(x => x.PracticalMarks.HasValue)
            .WithMessage("Practical marks must be between 0 and 100");

        // Custom validation rule to ensure theory or practical marks are set based on subject type
        RuleFor(x => x)
            .Must(x => ValidateMarksBasedOnType(x))
            .WithMessage("Theory or practical marks must be set based on subject type");
    }

    private bool ValidateMarksBasedOnType(CreateSubjectDto dto)
    {
        if (string.IsNullOrEmpty(dto.SubjectType) || dto.SubjectType == "Both")
        {
            return dto.TheoryMarks.HasValue && dto.PracticalMarks.HasValue;
        }
        else if (dto.SubjectType == "Theory")
        {
            return dto.TheoryMarks.HasValue;
        }
        else if (dto.SubjectType == "Practical")
        {
            return dto.PracticalMarks.HasValue;
        }

        return true;
    }
}
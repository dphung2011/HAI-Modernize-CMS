using CMS.Application.DTOs;
using FluentValidation;

namespace CMS.Application.Validators;

public class CreateMarkDtoValidator : AbstractValidator<CreateMarkDto>
{
    public CreateMarkDtoValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(45).WithMessage("First name cannot exceed 45 characters");

        RuleFor(x => x.LastName)
            .MaximumLength(45).WithMessage("Last name cannot exceed 45 characters");

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(45).WithMessage("Course name cannot exceed 45 characters");

        RuleFor(x => x.SemOrYear)
            .NotEmpty().WithMessage("Semester/Year is required")
            .MaximumLength(45).WithMessage("Semester/Year cannot exceed 45 characters");

        // Subject 1 validation
        RuleFor(x => x.Subject1)
            .MaximumLength(45).WithMessage("Subject name cannot exceed 45 characters");

        RuleFor(x => x.Subject1MaxTheoryMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject1MaxTheoryMarks))
            .WithMessage("Maximum theory marks must be a number between 0 and 999");

        RuleFor(x => x.Subject1TheoryMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject1TheoryMarks))
            .WithMessage("Theory marks must be a number between 0 and 999");

        RuleFor(x => x.Subject1MaxPracticalMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject1MaxPracticalMarks))
            .WithMessage("Maximum practical marks must be a number between 0 and 999");

        RuleFor(x => x.Subject1PracticalMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject1PracticalMarks))
            .WithMessage("Practical marks must be a number between 0 and 999");

        // Subject 2 validation
        RuleFor(x => x.Subject2)
            .MaximumLength(45).WithMessage("Subject name cannot exceed 45 characters");

        RuleFor(x => x.Subject2MaxTheoryMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject2MaxTheoryMarks))
            .WithMessage("Maximum theory marks must be a number between 0 and 999");

        RuleFor(x => x.Subject2TheoryMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject2TheoryMarks))
            .WithMessage("Theory marks must be a number between 0 and 999");

        RuleFor(x => x.Subject2MaxPracticalMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject2MaxPracticalMarks))
            .WithMessage("Maximum practical marks must be a number between 0 and 999");

        RuleFor(x => x.Subject2PracticalMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject2PracticalMarks))
            .WithMessage("Practical marks must be a number between 0 and 999");

        // Subject 3 validation
        RuleFor(x => x.Subject3)
            .MaximumLength(45).WithMessage("Subject name cannot exceed 45 characters");

        RuleFor(x => x.Subject3MaxTheoryMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject3MaxTheoryMarks))
            .WithMessage("Maximum theory marks must be a number between 0 and 999");

        RuleFor(x => x.Subject3TheoryMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject3TheoryMarks))
            .WithMessage("Theory marks must be a number between 0 and 999");

        RuleFor(x => x.Subject3MaxPracticalMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject3MaxPracticalMarks))
            .WithMessage("Maximum practical marks must be a number between 0 and 999");

        RuleFor(x => x.Subject3PracticalMarks)
            .Matches(@"^\d{1,3}$").When(x => !string.IsNullOrEmpty(x.Subject3PracticalMarks))
            .WithMessage("Practical marks must be a number between 0 and 999");

        // Custom validation for marks not exceeding maximum marks
        RuleFor(x => x)
            .Must(x => ValidateMarks(x))
            .WithMessage("Marks cannot exceed maximum marks");
    }

    private bool ValidateMarks(CreateMarkDto dto)
    {
        // Subject 1
        if (!string.IsNullOrEmpty(dto.Subject1TheoryMarks) && !string.IsNullOrEmpty(dto.Subject1MaxTheoryMarks))
        {
            if (int.TryParse(dto.Subject1TheoryMarks, out int marks) && 
                int.TryParse(dto.Subject1MaxTheoryMarks, out int maxMarks))
            {
                if (marks > maxMarks)
                {
                    return false;
                }
            }
        }

        if (!string.IsNullOrEmpty(dto.Subject1PracticalMarks) && !string.IsNullOrEmpty(dto.Subject1MaxPracticalMarks))
        {
            if (int.TryParse(dto.Subject1PracticalMarks, out int marks) && 
                int.TryParse(dto.Subject1MaxPracticalMarks, out int maxMarks))
            {
                if (marks > maxMarks)
                {
                    return false;
                }
            }
        }

        // Subject 2
        if (!string.IsNullOrEmpty(dto.Subject2TheoryMarks) && !string.IsNullOrEmpty(dto.Subject2MaxTheoryMarks))
        {
            if (int.TryParse(dto.Subject2TheoryMarks, out int marks) && 
                int.TryParse(dto.Subject2MaxTheoryMarks, out int maxMarks))
            {
                if (marks > maxMarks)
                {
                    return false;
                }
            }
        }

        if (!string.IsNullOrEmpty(dto.Subject2PracticalMarks) && !string.IsNullOrEmpty(dto.Subject2MaxPracticalMarks))
        {
            if (int.TryParse(dto.Subject2PracticalMarks, out int marks) && 
                int.TryParse(dto.Subject2MaxPracticalMarks, out int maxMarks))
            {
                if (marks > maxMarks)
                {
                    return false;
                }
            }
        }

        // Subject 3
        if (!string.IsNullOrEmpty(dto.Subject3TheoryMarks) && !string.IsNullOrEmpty(dto.Subject3MaxTheoryMarks))
        {
            if (int.TryParse(dto.Subject3TheoryMarks, out int marks) && 
                int.TryParse(dto.Subject3MaxTheoryMarks, out int maxMarks))
            {
                if (marks > maxMarks)
                {
                    return false;
                }
            }
        }

        if (!string.IsNullOrEmpty(dto.Subject3PracticalMarks) && !string.IsNullOrEmpty(dto.Subject3MaxPracticalMarks))
        {
            if (int.TryParse(dto.Subject3PracticalMarks, out int marks) && 
                int.TryParse(dto.Subject3MaxPracticalMarks, out int maxMarks))
            {
                if (marks > maxMarks)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
namespace CMS.Application.DTOs;

public class CreateSubjectDto
{
    public string? CourseName { get; set; }
    public string? SemOrYear { get; set; }
    public string? SubjectName { get; set; }
    public string? SubjectType { get; set; }
    public int? TheoryMarks { get; set; }
    public int? PracticalMarks { get; set; }
}
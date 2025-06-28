namespace CMS.Application.DTOs;

public class CreateCourseDto
{
    public string? CourseName { get; set; }
    public string? SemOrYear { get; set; }
    public int? TotalSemOrYear { get; set; }
}
namespace CMS.Application.DTOs;

public class CourseDto
{
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? SemOrYear { get; set; }
    public int? TotalSemOrYear { get; set; }
}
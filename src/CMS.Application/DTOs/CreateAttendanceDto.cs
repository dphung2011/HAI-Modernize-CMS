namespace CMS.Application.DTOs;

public class CreateAttendanceDto
{
    public string? Date { get; set; }
    public string? CourseName { get; set; }
    public string? SemOrYear { get; set; }
    public string? SubjectName { get; set; }
    public int StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Status { get; set; } = "Present";
}
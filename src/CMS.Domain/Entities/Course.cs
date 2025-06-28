namespace CMS.Domain.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? SemOrYear { get; set; }
    public int? TotalSemOrYear { get; set; }

    // Navigation properties
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
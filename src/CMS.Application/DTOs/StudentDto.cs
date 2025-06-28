namespace CMS.Application.DTOs;

public class StudentDto
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public string? DateOfBirth { get; set; }
    public int? Age { get; set; }
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }
    public string? ContactNumber { get; set; }
    public string? EmailId { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public double? Pincode { get; set; }
    public string? CourseName { get; set; }
    public string? AdmissionDate { get; set; }
    public string? ActiveStatus { get; set; }
}
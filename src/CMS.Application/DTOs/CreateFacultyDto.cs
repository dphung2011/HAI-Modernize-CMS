namespace CMS.Application.DTOs;

public class CreateFacultyDto
{
    public string? FacultyFirstName { get; set; }
    public string? FacultyLastName { get; set; }
    public string? Gender { get; set; }
    public string? DateOfBirth { get; set; }
    public int? Age { get; set; }
    public string? ContactNumber { get; set; }
    public string? EmailId { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public double? Pincode { get; set; }
    public string? Qualification { get; set; }
    public string? Experience { get; set; }
    public string? SubjectName { get; set; }
    public byte[]? ProfilePic { get; set; }
    public string? Password { get; set; }
    public string? ActiveStatus { get; set; } = "Active";
    public string? JoinDate { get; set; }
}
namespace CMS.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? EmailId { get; set; }
    public string? Phone { get; set; }
    public string? Role { get; set; }
}
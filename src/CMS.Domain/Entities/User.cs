namespace CMS.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? EmailId { get; set; }
    public string? Phone { get; set; }
    public string Password { get; set; } = string.Empty;
    public string? Role { get; set; } = "User";
}
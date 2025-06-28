namespace CMS.Application.DTOs;

public class TokenDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Role { get; set; }
    public DateTime Expiration { get; set; }
}
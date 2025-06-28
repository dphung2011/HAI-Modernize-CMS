using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IFacultyRepository _facultyRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IFacultyRepository facultyRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _facultyRepository = facultyRepository;
        _configuration = configuration;
    }

    public async Task<TokenDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
        
        if (user == null || !VerifyPassword(loginDto.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        return GenerateTokenForUser(user);
    }

    public async Task<TokenDto> LoginFacultyAsync(LoginDto loginDto)
    {
        var faculty = await GetFacultyByEmailAsync(loginDto.Username);
        
        if (faculty == null || !VerifyPassword(loginDto.Password, faculty.Password ?? string.Empty))
        {
            throw new UnauthorizedAccessException("Invalid faculty email or password");
        }

        return GenerateTokenForFaculty(faculty);
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        // In a real implementation, you would use a password hasher
        // For simplicity, we're doing a direct comparison
        return inputPassword == storedPassword;
    }

    private TokenDto GenerateTokenForUser(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? "defaultsecretkeythatneedstobeatleast32characters");
        var tokenExpirationMinutes = int.Parse(jwtSettings["AccessTokenExpirationMinutes"] ?? "60");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            }),
            Expires = DateTime.UtcNow.AddMinutes(tokenExpirationMinutes),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenDto
        {
            AccessToken = tokenString,
            Username = user.Username,
            Role = user.Role,
            Expiration = tokenDescriptor.Expires.Value
        };
    }

    private TokenDto GenerateTokenForFaculty(Faculty faculty)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? "defaultsecretkeythatneedstobeatleast32characters");
        var tokenExpirationMinutes = int.Parse(jwtSettings["AccessTokenExpirationMinutes"] ?? "60");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, faculty.EmailId ?? ""),
                new Claim(ClaimTypes.NameIdentifier, faculty.FacultyId.ToString()),
                new Claim(ClaimTypes.Role, "Faculty"),
                new Claim("FacultyName", $"{faculty.FacultyFirstName} {faculty.FacultyLastName}"),
                new Claim("SubjectName", faculty.SubjectName ?? "")
            }),
            Expires = DateTime.UtcNow.AddMinutes(tokenExpirationMinutes),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenDto
        {
            AccessToken = tokenString,
            Username = faculty.EmailId ?? "",
            Role = "Faculty",
            Expiration = tokenDescriptor.Expires.Value
        };
    }

    private async Task<Faculty?> GetFacultyByEmailAsync(string email)
    {
        // Get all faculties and filter by email
        var faculties = await _facultyRepository.GetAllAsync();
        return faculties.FirstOrDefault(f => f.EmailId == email);
    }
}
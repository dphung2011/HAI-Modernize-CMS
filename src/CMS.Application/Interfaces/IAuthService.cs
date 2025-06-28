using CMS.Application.DTOs;

namespace CMS.Application.Interfaces;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto loginDto);
    Task<TokenDto> LoginFacultyAsync(LoginDto loginDto);
}
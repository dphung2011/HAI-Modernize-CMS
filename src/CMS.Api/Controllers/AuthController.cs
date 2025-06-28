using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers;

/// <summary>
/// Controller for handling authentication operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token
    /// </summary>
    /// <param name="loginDto">The login credentials</param>
    /// <returns>A JWT token if authentication is successful</returns>
    /// <response code="200">Returns the JWT token</response>
    /// <response code="401">If the credentials are invalid</response>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    /// <summary>
    /// Authenticates a faculty member and returns a JWT token
    /// </summary>
    /// <param name="loginDto">The login credentials</param>
    /// <returns>A JWT token if authentication is successful</returns>
    /// <response code="200">Returns the JWT token</response>
    /// <response code="401">If the credentials are invalid</response>
    [HttpPost("faculty/login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenDto>> LoginFaculty([FromBody] LoginDto loginDto)
    {
        try
        {
            var result = await _authService.LoginFacultyAsync(loginDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
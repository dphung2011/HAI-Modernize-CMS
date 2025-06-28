using CMS.Application.DTOs;
using CMS.Application.Services;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CMS.UnitTests.Application.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IFacultyRepository> _mockFacultyRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly Mock<IConfigurationSection> _mockConfigurationSection;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockFacultyRepository = new Mock<IFacultyRepository>();
        _mockConfiguration = new Mock<IConfiguration>();
        _mockConfigurationSection = new Mock<IConfigurationSection>();

        _mockConfigurationSection.Setup(x => x["Secret"]).Returns("ThisIsAVerySecureKeyThatShouldBeAtLeast32Characters");
        _mockConfigurationSection.Setup(x => x["Issuer"]).Returns("CMS.Api");
        _mockConfigurationSection.Setup(x => x["Audience"]).Returns("CMS.Client");
        _mockConfigurationSection.Setup(x => x["AccessTokenExpirationMinutes"]).Returns("60");

        _mockConfiguration.Setup(x => x.GetSection("JwtSettings")).Returns(_mockConfigurationSection.Object);

        _authService = new AuthService(_mockUserRepository.Object, _mockFacultyRepository.Object, _mockConfiguration.Object);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Username = "admin",
            Password = "admin123"
        };

        var user = new User
        {
            Id = 1,
            Username = "admin",
            Password = "admin123",
            Role = "Admin"
        };

        _mockUserRepository.Setup(x => x.GetByUsernameAsync("admin")).ReturnsAsync(user);

        // Act
        var result = await _authService.LoginAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);
        Assert.Equal("Admin", result.Role);
        Assert.NotEmpty(result.AccessToken);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorizedAccessException_WhenCredentialsAreInvalid()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Username = "admin",
            Password = "wrongpassword"
        };

        var user = new User
        {
            Id = 1,
            Username = "admin",
            Password = "admin123",
            Role = "Admin"
        };

        _mockUserRepository.Setup(x => x.GetByUsernameAsync("admin")).ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await _authService.LoginAsync(loginDto));
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorizedAccessException_WhenUserDoesNotExist()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Username = "nonexistent",
            Password = "password"
        };

        _mockUserRepository.Setup(x => x.GetByUsernameAsync("nonexistent")).ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await _authService.LoginAsync(loginDto));
    }
}
using CMS.Api;
using CMS.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace CMS.IntegrationTests.Controllers;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var client = _factory.CreateClient();
        var loginDto = new LoginDto
        {
            Username = "admin",
            Password = "admin123"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(loginDto),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PostAsync("/api/auth/login", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var tokenDto = await response.Content.ReadFromJsonAsync<TokenDto>(_jsonOptions);
        Assert.NotNull(tokenDto);
        Assert.Equal("admin", tokenDto.Username);
        Assert.Equal("Admin", tokenDto.Role);
        Assert.NotEmpty(tokenDto.AccessToken);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var client = _factory.CreateClient();
        var loginDto = new LoginDto
        {
            Username = "admin",
            Password = "wrongpassword"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(loginDto),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PostAsync("/api/auth/login", content);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
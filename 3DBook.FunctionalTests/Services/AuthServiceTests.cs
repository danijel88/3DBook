using _3DBook.Core;
using _3DBook.Models.AuthViewModel;
using _3DBook.UseCases.UserAggregate.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace _3DBook.FunctionalTests.Services;

public class AuthServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IHttpContextAccessor> _contextAccessorMock;
    private readonly Mock<IUserClaimsPrincipalFactory<User>> _userClaimsPrincipalFactoryMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        // Mock UserManager, which is needed for SignInManager
        _userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(),
            null, null, null, null, null, null, null, null);
        _contextAccessorMock = new Mock<IHttpContextAccessor>();
        _userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
        _loggerMock = new Mock<ILogger<AuthService>>();
        // Mock SignInManager using mocked UserManager and other dependencies
        _signInManagerMock = new Mock<SignInManager<User>>(
            _userManagerMock.Object,
            _contextAccessorMock.Object,
            _userClaimsPrincipalFactoryMock.Object,
            null, null, null, null);
        // Assuming AccountService is the class where LoginAsync is defined
        _authService = new AuthService(_signInManagerMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Login_ReturnSuccess_WhenLoginSucceeds()
    {
        var loginModel = new LoginViewModel
        {
            Email = "admin@company.rs",
            Password = "P@s$w0rd2024!"
        };
        _signInManagerMock.Setup(s => s.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false))
            .ReturnsAsync(SignInResult.Success);
        var result = await _authService.LoginAsync(loginModel);
        Assert.True(result.IsSuccess);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString().Contains($"User {loginModel.Email} has been logged")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ReturnsError_WhenLoginFails()
    {
        // Arrange
        var model = new LoginViewModel { Email = "test@example.com", Password = "InvalidPassword" };
        _signInManagerMock
            .Setup(x => x.PasswordSignInAsync(model.Email, model.Password, false, false))
            .ReturnsAsync(SignInResult.Failed);
        // Act
        var result = await _authService.LoginAsync(model);
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid login attempt.", result.Errors.FirstOrDefault());
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Never);
    }

    [Fact]
    public async Task LogoutAsync_CallsSignOutAsync_AndLogsInformation()
    {
        // Act
        await _authService.LogoutAsync();
        // Assert
        _signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("User logged out")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}
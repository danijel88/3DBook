using _3DBook.Core;
using _3DBook.Models.AccountViewModel;
using _3DBook.UseCases.AccountsAggregate;
using _3DBook.UseCases.UserAggregate.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;

namespace _3DBook.FunctionalTests.Services;

public class AccountServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IHttpContextAccessor> _contextAccessorMock;
    private readonly Mock<ILogger<AccountService>> _loggerMock;
    private readonly IAccountService _accountService;

    public AccountServiceTests()
    {
        _userManagerMock = CreateUserManagerMock();
        _contextAccessorMock = new Mock<IHttpContextAccessor>();
        _loggerMock = new Mock<ILogger<AccountService>>();
        _accountService = new AccountService(_userManagerMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllAccountsAsync_ReturnsAllActiveAndInactiveAccounts()
    {
        // Arrange
        var users = GetSampleUsers();
        var mockUsers = users.BuildMock();
        _userManagerMock.Setup(u => u.Users).Returns(mockUsers);
        
        // Act
        var result = await _accountService.GetAllAccountsAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.Email == "admin@company1.rs" && r.IsActive == true);
        Assert.Contains(result, r => r.Email == "member@company1.rs" && r.IsActive == false);
        
    }

    private static Mock<UserManager<User>> CreateUserManagerMock()
    {
        return new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(),
            null, null, null, null, null, null, null, null);
    }

    private List<User> GetSampleUsers()
    {
        return new List<User>
            {
                new User
                {
                    Company = "Company 1",
                    Email = "admin@company1.rs",
                    FirstName = "Admin",
                    LastName = "Super",
                    IsActive = true
                },
                new User
                {
                    Company = "Company 1",
                    Email = "member@company1.rs",
                    FirstName = "Member",
                    LastName = "Super",
                    IsActive = false
                }
            };
    }
}

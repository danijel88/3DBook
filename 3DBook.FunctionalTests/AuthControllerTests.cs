using _3DBook.Controllers;
using _3DBook.Models.AuthViewModel;
using _3DBook.UseCases.UserAggregate.Auth;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace _3DBook.FunctionalTests;

public class AuthControllerTests
{
    [Fact]
    public async Task Login_ReturnAsViewResult_WithLoggedUser()
    {
        //Arrange
        var loginModel = new LoginViewModel
        {
            Email = "admin@company.rs",
            Password = "P@s$w0rd2024!"

        };
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(auth => auth.LoginAsync(loginModel)).ReturnsAsync(Result.Success);
        
        var controller = new AuthController(mockAuthService.Object);
        //Act
        var result = await controller.Login(loginModel);
        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<LoginViewModel>(viewResult.ViewData.Model);
        Assert.NotNull(model);
        Assert.Equal(loginModel.Email,model.Email);
    }

}
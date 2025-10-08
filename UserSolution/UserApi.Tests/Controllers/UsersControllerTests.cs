using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UserApi.Controllers;
using UserApi.Services;

namespace UserApi.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        [Test]
        public async Task GetName_ReturnsOkWithName()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(s => s.GetUserNameAsync(1)).ReturnsAsync("Alice");

            var controller = new UsersController(mockService.Object);

            // Act
            var actionResult = await controller.GetName(1);

            // Assert
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("Alice", okResult!.Value);
        }
    }
}

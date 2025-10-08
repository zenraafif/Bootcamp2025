using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Repositories;
using UserApi.Services;

namespace UserApi.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public async Task GetUserNameAsync_ReturnsName_WhenUserExists()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(1))
                    .ReturnsAsync(new User { Id = 1, Name = "Alice" });

            var service = new UserService(mockRepo.Object);

            // Act
            var name = await service.GetUserNameAsync(1);

            // Assert
            Assert.AreEqual("Alice", name);
            mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Test]
        public async Task GetUserNameAsync_ReturnsUnknown_WhenUserMissing()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((User?)null);

            var service = new UserService(mockRepo.Object);

            var name = await service.GetUserNameAsync(42);

            Assert.AreEqual("Unknown", name);
        }
    }
}

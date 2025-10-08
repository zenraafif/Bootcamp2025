using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using UserApi.Data;
using UserApi.Models;
using UserApi.Repositories;

namespace UserApi.Tests.Repositories
{
    [TestFixture]
    public class RepositoryIntegrationTests
    {
        [Test]
        public async Task AddAndGet_Works_WithInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
                .Options;

            // create & use context
            using (var context = new AppDbContext(options))
            {
                var repo = new UserRepository(context);
                await repo.AddAsync(new User { Id = 1, Name = "Bob" });

                var user = await repo.GetByIdAsync(1);
                Assert.IsNotNull(user);
                Assert.AreEqual("Bob", user!.Name);
            }
        }
    }
}

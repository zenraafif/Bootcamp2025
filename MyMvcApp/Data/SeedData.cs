using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;

namespace MyMvcApp.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext contexty)
        {
            // Automatically create the database if it doesn't exist
            contexty.Database.Migrate();

            // Skip seeding if any users already exist
            if (contexty.Users.Any())
                return;

            // Add default users
            contexty.Users.AddRange(
                new User { Name = "Alice", Email = "alice@example.com" },
                new User { Name = "Bob", Email = "bob@example.com" },
                new User { Name = "Charlie", Email = "charlie@example.com" }
            );

            contexty.SaveChanges();
        }
    }
}

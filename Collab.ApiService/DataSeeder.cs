using Collab.ApiService.Contexts;
using Collab.ApiService.Models;

namespace Collab.ApiService;

public static class DataSeeder
{
    public static void SeedData(AppDbContext context, IServiceProvider services)
    {
        if (context.Users.Any()) return; // Check if data is already seeded

        context.Users.AddRange(
            new User { username = "admin", password = "admin" }
        );

        context.SaveChanges();
    }
}


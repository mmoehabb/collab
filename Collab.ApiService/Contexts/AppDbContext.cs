using Microsoft.EntityFrameworkCore;
using Collab.ApiService.Models;

namespace Collab.ApiService.Contexts;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
}

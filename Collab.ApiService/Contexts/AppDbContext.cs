using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Collab.ApiService.Models;

namespace Collab.ApiService.Contexts;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
	}

	public DbSet<Channel> Channels { get; set; }
	public DbSet<Message> Messages { get; set; }
	public DbSet<ReadMessage> ReadMessages { get; set; }
}

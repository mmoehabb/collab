using Collab.ApiService.Contexts;
using Collab.ApiService.Interfaces;
using Collab.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace Collab.ApiService.Repositories;

public class UserRepository : IUserRepository
{
	private AppDbContext _ctx;

	public UserRepository(AppDbContext ctx) {
		this._ctx = ctx;
	}

	public IEnumerable<User> GetAll() {
		return this._ctx.Users.AsEnumerable();
	}
}

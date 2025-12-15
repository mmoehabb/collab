using Collab.ApiService.Contexts;
using Collab.ApiService.Interfaces;
using Collab.ApiService.Models;
using Collab.ApiService.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Collab.ApiService.Repositories;

public class UserRepository : IUserRepository
{
	private AppDbContext _ctx;

	public UserRepository(AppDbContext ctx) {
		this._ctx = ctx;
	}

	public IEnumerable<UserDto> GetAll() {
		var res =
			from user in this._ctx.Users.AsQueryable()
			select new { Id = user.Id, UserName = user.UserName, Email = user.Email };
		return res.ToList().Select(u => new UserDto(u.Id, u.Email, u.UserName));
	}
}

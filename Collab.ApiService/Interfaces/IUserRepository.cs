using System.Collections;
using Collab.ApiService.DTOs;
using Collab.ApiService.Models;
using Microsoft.AspNetCore.Identity;

namespace Collab.ApiService.Interfaces;

public interface IUserRepository
{
	IEnumerable<UserDto> GetAll();
	UserDto? GetOne(string id);
	/// Unsafe, should only be used internally, never dispatch its result to the client.
	User? GetOneUnsafe(string id);
	bool Contains(string id);
}

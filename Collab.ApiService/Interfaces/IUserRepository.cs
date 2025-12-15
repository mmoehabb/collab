using System.Collections;
using Collab.ApiService.DTOs;
using Collab.ApiService.Models;
using Microsoft.AspNetCore.Identity;

namespace Collab.ApiService.Interfaces;

public interface IUserRepository
{
	IEnumerable<UserDto> GetAll();
}

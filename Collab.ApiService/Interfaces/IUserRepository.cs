using System.Collections;
using Collab.ApiService.Models;

namespace Collab.ApiService.Interfaces;

public interface IUserRepository
{
	IEnumerable<User> GetAll();
}

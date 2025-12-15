using System.Text.Json;
using Collab.ApiService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Collab.ApiService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
	private readonly IUserRepository _repo;

	public UsersController(IUserRepository repo)
	{
		this._repo = repo;
	}

	[HttpGet("all")]
	public async Task<ActionResult> GetAll() {
		return Ok(this._repo.GetAll());
	}
}

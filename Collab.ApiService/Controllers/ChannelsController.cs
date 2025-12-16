using System.Text.Json;
using Collab.ApiService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;

namespace Collab.ApiService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ChannelsController : ControllerBase
{
	private readonly IChannelRepository _repo;

	public ChannelsController(IChannelRepository repo)
	{
		this._repo = repo;
	}

	[HttpGet("all")]
	[Authorize]
	public async Task<ActionResult> GetAll()
	{
		return Ok(this._repo.GetAll());
	}

	[HttpPost("create")]
	[Authorize]
	public async Task<ActionResult> Create(string title, string? description)
	{
		var res = this._repo.Add(title, description);
		return Ok(res);
	}

	[HttpPatch("update")]
	[Authorize]
	public async Task<ActionResult> Update(int id, string? title, string? description)
	{
		if (!this._repo.Contains(id)) return NotFound();
		var res = this._repo.Update(id, title, description);
		return Ok(res);
	}

	[HttpDelete("delete")]
	[Authorize]
	public async Task<ActionResult> Delete(int id)
	{
		if (!this._repo.Contains(id)) return NotFound();
		this._repo.Remove(id);
		return Ok();
	}
}

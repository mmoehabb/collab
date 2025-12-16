using System.Security.Claims;
using System.Text.Json;
using Collab.ApiService.Interfaces;
using Collab.ApiService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collab.ApiService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly ILogger<MessagesController> _logger;
	private readonly IMessageRepository _repo;
	private readonly IUserRepository _userRepo;
	private readonly IChannelRepository _chnRepo;

	public MessagesController(
		ILogger<MessagesController> logger,
		IMessageRepository msgRepo,
		IUserRepository userRepo,
		IChannelRepository chnRepo
	)
	{
		this._logger = logger;
		this._repo = msgRepo;
		this._userRepo = userRepo;
		this._chnRepo = chnRepo;
	}

	[HttpGet("all")]
	[Authorize]
	public async Task<ActionResult> GetAll(int channelId)
	{
		return Ok(this._repo.GetAll(channelId));
	}

	[HttpPost("create")]
	[Authorize]
	public async Task<ActionResult> Create(int channelId, string[] msgs)
	{
		// Return NotFound in case the user does not exist
		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		if (userId == null) return Unauthorized();
		var user = this._userRepo.GetOneUnsafe(userId!);
		if (user == null) return NotFound("User Not Found!");

		// Return NotFound in case the channel does not exist
		var channel = this._chnRepo.GetOneUnsafe(channelId);
		if (channel == null) return NotFound("Channel Not Found!");

		var messages = msgs.Select(content => new Message{
			Sender = user,
			Channel = channel,
			Content = content,
		}).ToList();

		this._repo.Add(messages);

		return Ok();
	}
}

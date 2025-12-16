using Collab.ApiService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Collab.ApiService.Hubs;

public class ChannelHub : Hub
{
	public async Task SendMessage(string userId, string message)
	{
		await Clients.All.SendAsync("new-message", userId, message);
	}
}

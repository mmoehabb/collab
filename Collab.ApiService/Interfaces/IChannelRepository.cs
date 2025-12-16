using System.Collections;
using Collab.ApiService.DTOs;
using Collab.ApiService.Models;
using Microsoft.AspNetCore.Identity;

namespace Collab.ApiService.Interfaces;

public interface IChannelRepository
{
	IEnumerable<ChannelDto> GetAll();
	ChannelDto? GetOne(int id);
	Channel? GetOneUnsafe(int id);
	bool Contains(int id);
	ChannelDto Add(string title, string? description);
	ChannelDto Update(int id, string? title, string? description);
	void Remove(int id);
}

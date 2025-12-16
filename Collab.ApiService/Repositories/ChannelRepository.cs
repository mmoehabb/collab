using Collab.ApiService.Contexts;
using Collab.ApiService.Interfaces;
using Collab.ApiService.Models;
using Collab.ApiService.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Collab.ApiService.Repositories;

public class ChannelRepository : IChannelRepository
{
	private AppDbContext _ctx;

	public ChannelRepository(AppDbContext ctx)
	{
		this._ctx = ctx;
	}

	public IEnumerable<ChannelDto> GetAll()
	{
		return this._ctx.Channels.Select(ch => new ChannelDto(
			ch.Id,
			ch.Title,
			ch.Description,
			ch.CreatedAt
		));
	}

	public ChannelDto? GetOne(int id)
	{
		var ch = this._ctx.Channels.SingleOrDefault(c => c.Id == id);
		if (ch == null) return null;
		return new ChannelDto(
			ch.Id,
			ch.Title,
			ch.Description,
			ch.CreatedAt
		);
	}

	public Channel? GetOneUnsafe(int id)
	{
		return this._ctx.Channels.SingleOrDefault(c => c.Id == id);
	}

	public bool Contains(int id)
	{
		return this._ctx.Channels.Any(c => c.Id == id);
	}

	public ChannelDto Add(string title, string? description)
	{
		var res = this._ctx.Channels.Add(new Channel{
			Title = title,
			Description = description
		});
		this._ctx.SaveChanges();
		return new ChannelDto(
			res.Entity.Id,
			res.Entity.Title,
			res.Entity.Description,
			res.Entity.CreatedAt
		);
	}

	public ChannelDto Update(int id, string? title, string? description)
	{
		var channel = this._ctx.Channels.Single(c => c.Id == id);
		channel.Title = title ?? channel.Title;
		channel.Description = description ?? channel.Description;
		this._ctx.SaveChanges();
		return new ChannelDto(
			channel.Id,
			channel.Title,
			channel.Description,
			channel.CreatedAt
		);
	}

	public void Remove(int id)
	{
		var res = this._ctx.Channels.Remove(new Channel{ Id = id, Title = "" });
		this._ctx.SaveChanges();
	}
}

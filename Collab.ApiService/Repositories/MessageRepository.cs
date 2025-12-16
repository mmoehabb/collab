using Collab.ApiService.Contexts;
using Collab.ApiService.Interfaces;
using Collab.ApiService.Models;
using Collab.ApiService.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Collab.ApiService.Repositories;

public class MessageRepository : IMessageRepository
{
	private AppDbContext _ctx;

	public MessageRepository(AppDbContext ctx) {
		this._ctx = ctx;
	}

	public IEnumerable<MessageDto> GetAll(int channelId) {
		var res =
			from message in this._ctx.Messages.AsQueryable()
			where message.Channel.Id == channelId
			select message;

		return res.Select(msg =>
			new MessageDto(
				msg.Id,
				msg.Content,
				msg.Sender.Id,
				msg.Channel.Id,
				msg.SentAt,
				msg.UpdatedAt
			)
		);
	}

	public MessageDto? GetOne(int id)
	{
		var msg = this._ctx.Messages.SingleOrDefault(c => c.Id == id);
		if (msg == null) return null;
		return new MessageDto(
			msg.Id,
			msg.Content,
			msg.Sender.Id,
			msg.Channel.Id,
			msg.SentAt,
			msg.UpdatedAt
		);
	}

	public bool Contains(int id)
	{
		return this._ctx.Messages.Any(c => c.Id == id);
	}

	public void Add(ICollection<Message> msgs)
	{
		this._ctx.Messages.AddRange(msgs);
		this._ctx.SaveChanges();
	}

	public void Update(int id, string content)
	{
		var message = this._ctx.Messages.Single(c => c.Id == id);
		message.Content = content ?? message.Content;
		this._ctx.SaveChanges();
	}

	public void Remove(int id)
	{
		var found =  this._ctx.Messages.Single(c => c.Id == id);
		if (found == null) return;
		this._ctx.Messages.Remove(found);
		this._ctx.SaveChanges();
	}
}

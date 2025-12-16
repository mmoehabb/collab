using System.Collections;
using Collab.ApiService.Models;
using Collab.ApiService.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Collab.ApiService.Interfaces;

public interface IMessageRepository
{
	IEnumerable<MessageDto> GetAll(int channelId);
	MessageDto? GetOne(int msgId);
	bool Contains(int id);
	void Add(ICollection<Message> msgs);
	void Update(int id, string content);
	void Remove(int id);
}

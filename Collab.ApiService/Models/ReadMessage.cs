using Microsoft.EntityFrameworkCore;

namespace Collab.ApiService.Models;

[PrimaryKey(nameof(MessagesId), nameof(UsersId))]
public class ReadMessage {
    public int MessagesId { get; set; }
    public int UsersId { get; set; }
    public Message Message { get; set; } = null!;
    public User User { get; set; } = null!;
}

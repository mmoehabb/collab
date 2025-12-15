namespace Collab.ApiService.Models;

public class Message {
	public int Id { get; set; }
	public required string Content { get; set; }
	public required User Sender { get; set; }
	public required Channel Channel { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public List<ReadMessage> ReadMessages { get; } = [];
}

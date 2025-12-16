namespace Collab.ApiService.DTOs;

public record MessageDto(
	int Id,
	string Content,
	string SenderId,
	int ChannelId,
	DateTime SentAt,
	DateTime UpdatedAt
);

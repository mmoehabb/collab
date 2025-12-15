namespace Collab.ApiService.Models;

public class User
{
	public int Id { get; set; }
	public required string username { get; set; }
	public required string password { get; set; }
	public string? token { get; set; }
}


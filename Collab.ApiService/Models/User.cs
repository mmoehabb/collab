using Microsoft.AspNetCore.Identity;

namespace Collab.ApiService.Models;

public class User : IdentityUser
{
    public List<ReadMessage> ReadMessages { get; } = [];
}

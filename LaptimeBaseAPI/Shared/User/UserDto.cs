using Shared.Team;

namespace Shared.User;

public class UserDto
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
    
    public required string Password { get; set; }

    public bool IsAdmin { get; set; } = false;
    
    public List<TeamDto> Teams { get; set; }
}
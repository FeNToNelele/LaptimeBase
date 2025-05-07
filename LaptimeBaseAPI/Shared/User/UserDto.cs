using Shared.Team;

namespace Shared.User;

public class UserDto
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public bool IsAdmin { get; set; }
    
    public List<TeamDto> Teams { get; set; }
}
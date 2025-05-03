using Shared.Car;
using Shared.Laptime;
using Shared.User;

namespace Shared.Team;

public class TeamDto
{
    public int UserId { get; set; }
    
    public UserDto User { get; set; }
    
    public int CarId { get; set; }
    
    public CarDto Car { get; set; }
    
    public string Name { get; set; }
    
    public List<LaptimeDto> Laptimes { get; set; }
}
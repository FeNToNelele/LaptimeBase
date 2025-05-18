using Shared.Car;

namespace Shared.Team;

public class TeamDto
{
    public int Id { get; set; }

    public CarDto Car { get; set; }
    
    public string Name { get; set; }
}
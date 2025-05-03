using Shared.Team;

namespace Shared.Car;

public class CarDto
{
    public int Id { get; set; }
    
    public string Class { get; set; }
    
    public List<TeamDto> Teams { get; set; }
}
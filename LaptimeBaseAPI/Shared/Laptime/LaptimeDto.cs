using Shared.Team;

namespace Shared.Laptime;

public class LaptimeDto
{
    public int Id { get; set; }
    
    public TimeSpan Time { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public TeamDto Team { get; set; }
}
using Shared.Laptime;
using Shared.Track;

namespace Shared.Session;

public class SessionDto
{
    public int Id { get; set; }
    
    public DateTime HeldAt { get; set; }
    
    public int AmbientTemp { get; set; }
    
    public int TrackTemp { get; set; }
    
    public int TrackId { get; set; }
    
    public TrackDto Track { get; set; }
}
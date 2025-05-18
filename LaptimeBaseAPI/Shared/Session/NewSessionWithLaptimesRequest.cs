using System.ComponentModel.DataAnnotations;
using Shared.Laptime;

namespace Shared.Session;

public class NewSessionWithLaptimesRequest
{
    public DateTime HeldAt { get; set; }

    [Range(0, 100)]
    public int AmbientTemp { get; set; }

    [Range(0, 100)]
    public int TrackTemp { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a track.")]
    public int TrackId { get; set; }
    
    [Required]
    [MinLength(1)]
    public List<NewLapRequest> NewLapRequests { get; set; }
}
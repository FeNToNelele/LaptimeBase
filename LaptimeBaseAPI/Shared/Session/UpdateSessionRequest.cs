namespace Shared.Session;

public class UpdateSessionRequest
{
    public DateTime HeldAt { get; set; }

    public int AmbientTemp { get; set; }

    public int TrackTemp { get; set; }

    public int TrackId { get; set; }
} 
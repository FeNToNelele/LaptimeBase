namespace Shared.Session;

public class NewSessionRequest
{
    public DateTime HeldAt { get; set; }

    public int AmbientTemp { get; set; }

    public int TrackTemp { get; set; }

    public int TrackId { get; set; }

    public List<int> LaptimeIds { get; set; }
}

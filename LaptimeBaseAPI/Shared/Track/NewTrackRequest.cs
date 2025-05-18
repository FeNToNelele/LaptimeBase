using System.ComponentModel.DataAnnotations;

namespace Shared.Track;

public class NewTrackRequest
{
    [Required]
    public string Name { get; set; }
}

using Refit;
using Shared.Track;

namespace WebUI.Services;

public interface ITrackService
{
    [Get("/api/tracks")]
    Task<List<TrackDto>> GetTracksAsync();
    
    [Post("/api/tracks")]
    Task<TrackDto> CreateTrackAsync([Body] NewTrackRequest newTrackRequest);
}
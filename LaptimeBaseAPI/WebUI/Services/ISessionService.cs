using Refit;
using Shared.Laptime;
using Shared.Session;

namespace WebUI.Services;

public interface ISessionService
{
    [Get("/api/sessions")]
    Task<List<SessionDto>> GetAllSessionsAsync();
    
    [Get("/api/sessions/{id}")]
    Task<SessionDto> GetSessionAsync(int id);
}
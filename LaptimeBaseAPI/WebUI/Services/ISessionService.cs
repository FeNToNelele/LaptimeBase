using Refit;
using Shared.Session;

namespace WebUI.Services;

public interface ISessionService
{
    [Get("/api/sessions")]
    Task<List<SessionDto>> GetAllSessions();
}
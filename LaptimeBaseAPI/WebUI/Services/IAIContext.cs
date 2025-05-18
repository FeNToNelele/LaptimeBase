using Shared.Session;

namespace WebUI.Services;

public interface IAIContext
{
    SessionDto? Session { get; set; }
}
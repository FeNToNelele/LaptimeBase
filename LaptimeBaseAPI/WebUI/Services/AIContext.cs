using Shared.Session;

namespace WebUI.Services;

public class AIContext : IAIContext
{
    public SessionDto? Session { get; set; }
}
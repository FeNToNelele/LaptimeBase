using Refit;
using Shared.Team;

namespace WebUI.Services;

public interface ITeamService
{
    [Get("/api/teams")]
    Task<List<TeamDto>> GetTeamsAsync();
}
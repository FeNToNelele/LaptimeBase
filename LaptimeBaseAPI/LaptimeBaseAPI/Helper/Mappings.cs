using LaptimeBaseAPI.Models;
using Shared.Car;
using Shared.Laptime;
using Shared.Session;
using Shared.Team;
using Shared.Track;

namespace LaptimeBaseAPI.Helper;

public static class Mappings
{
    public static CarDto ToCarDto(this Car car)
    {
        return new CarDto
        {
            Id = car.Id,
            Class = car.Class,
        };
    }
    
    public static TeamDto ToTeamDto(this Team team)
    {
        return new TeamDto
        {
            Car = team.Car?.ToCarDto(),
            Name = team.Name,
        };
    }

    public static LaptimeDto ToLaptimeDto(this Laptime laptime)
    {
        return new LaptimeDto
        {
            Id = laptime.Id,
            Time = laptime.Time,
            CreatedAt = laptime.CreatedAt,
            Team = laptime.Team?.ToTeamDto(),
        };
    }

    public static SessionDto ToSessionDto(this Session session)
    {
        return new SessionDto
        {
            Id = session.Id,
            HeldAt = session.HeldAt,
            AmbientTemp = session.AmbientTemp,
            TrackTemp = session.TrackTemp,
            Track = session.Track.ToTrackDto(),
            Laptimes = session.Laptimes?.Select(x => x.ToLaptimeDto()).ToList(),
        };
    }

    public static TrackDto ToTrackDto(this Track track)
    {
        return new TrackDto
        {
            Id = track.Id,
            Name = track.Name,
        };
    }
}
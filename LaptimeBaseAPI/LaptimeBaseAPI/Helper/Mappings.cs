using LaptimeBaseAPI.Models;
using Shared.Car;
using Shared.Laptime;
using Shared.Session;
using Shared.Team;
using Shared.Track;
using Shared.User;

namespace LaptimeBaseAPI.Helper;

public static class Mappings
{
    public static Car ToCarModel(this CarDto carDto)
    {
        return new Car
        {
            Id = carDto.Id,
            Class = carDto.Class,
            Teams = carDto.Teams.Select(ToTeamModel).ToList(),
        };
    }

    public static Car ToCarModel(this NewCarRequest newCar)
    {
        return new Car
        {
            Class = newCar.Class,
        };
    }
    
    public static CarDto ToCarDto(this Car car)
    {
        return new CarDto
        {
            Id = car.Id,
            Class = car.Class,
            Teams = car.Teams.Select(ToTeamDto).ToList(),
        };
    }
    
    public static Team ToTeamModel(this TeamDto teamDto)
    {
        return new Team
        {
            UserId = teamDto.UserId,
            User = teamDto.User.ToUserModel(),
            CarId = teamDto.CarId,
            Car = teamDto.Car.ToCarModel(),
            Name = teamDto.Name,
            Laptimes = teamDto.Laptimes.Select(ToLaptimeModel).ToList()
        };
    }

    public static TeamDto ToTeamDto(this Team team)
    {
        return new TeamDto
        {
            UserId = team.UserId,
            User = team.User.ToUserDto(),
            CarId = team.CarId,
            Car = team.Car.ToCarDto(),
            Name = team.Name,
            Laptimes = team.Laptimes.Select(ToLaptimeDto).ToList()
        };
    }

    public static User ToUserModel(this UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Username = userDto.Username,
            Password = userDto.Password,
            IsAdmin = userDto.IsAdmin,
            Teams = userDto.Teams.Select(ToTeamModel).ToList()
        };
    }

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
            IsAdmin = user.IsAdmin,
            Teams = user.Teams.Select(ToTeamDto).ToList()
        };
    }    
    public static Laptime ToLaptimeModel(this LaptimeDto laptimeDto)
    {
        return new Laptime
        {
            Id = laptimeDto.Id,
            Time = laptimeDto.Time,
            CreatedAt = laptimeDto.CreatedAt,
            TeamId = laptimeDto.TeamId,
            Team = laptimeDto.Team.ToTeamModel(),
            SessionId = laptimeDto.SessionId,
            Session = laptimeDto.Session.ToSessionModel()
        };
    }

    public static LaptimeDto ToLaptimeDto(this Laptime laptime)
    {
        return new LaptimeDto
        {
            Id = laptime.Id,
            Time = laptime.Time,
            CreatedAt = laptime.CreatedAt,
            TeamId = laptime.TeamId,
            Team = laptime.Team.ToTeamDto(),
            SessionId = laptime.SessionId,
            Session = laptime.Session.ToSessionDto()
        };
    }
    
    public static Session ToSessionModel(this SessionDto sessionDto)
    {
        return new Session
        {
            Id = sessionDto.Id,
            HeldAt = sessionDto.HeldAt,
            AmbientTemp = sessionDto.AmbientTemp,
            TrackTemp = sessionDto.TrackTemp,
            TrackId = sessionDto.TrackId,
            Track = sessionDto.Track.ToTrackModel(),
            Laptimes = sessionDto.Laptimes.Select(ToLaptimeModel).ToList()
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
            TrackId = session.TrackId,
            Track = session.Track.ToTrackDto(),
            Laptimes = session.Laptimes.Select(ToLaptimeDto).ToList()
        };
    }

    public static Track ToTrackModel(this TrackDto trackDto)
    {
        return new Track
        {
            Id = trackDto.Id,
            Name = trackDto.Name,
            Sessions = trackDto.Sessions.Select(ToSessionModel).ToList(),
        };
    }

    public static TrackDto ToTrackDto(this Track track)
    {
        return new TrackDto
        {
            Id = track.Id,
            Name = track.Name,
            Sessions = track.Sessions.Select(ToSessionDto).ToList(),
        };
    }
}
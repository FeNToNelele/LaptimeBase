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
    
    public static Team ToTeamModel(this NewTeamRequest newTeamRequest)
    {
        return new Team
        {
            UserId = newTeamRequest.UserId,
            CarId = newTeamRequest.CarId,
            Name = newTeamRequest.Name
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

    public static User ToUserModel(this NewUserRequest userDto)
    {
        return new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
            IsAdmin = userDto.IsAdmin
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
    public static Laptime ToLaptimeModel(this NewLapRequest newLapRequest)
    {
        return new Laptime
        {
            Time = newLapRequest.Laptime,
            CreatedAt = DateTime.Now,
            TeamId = newLapRequest.TeamId,
            SessionId = newLapRequest.SessionId
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
    
    public static Session ToSessionModel(this NewSessionRequest sessionRequest)
    {
        return new Session
        {
            HeldAt = sessionRequest.HeldAt,
            AmbientTemp = sessionRequest.AmbientTemp,
            TrackTemp = sessionRequest.TrackTemp,
            TrackId = sessionRequest.TrackId,
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
        };
    }

    public static Track ToTrackModel(this TrackDto trackDto)
    {
        return new Track
        {
            Id = trackDto.Id,
            Name = trackDto.Name,
        };
    }

    public static Track ToTrackModel(this NewTrackRequest request)
    {
        return new Track
        {
            Name = request.Name
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
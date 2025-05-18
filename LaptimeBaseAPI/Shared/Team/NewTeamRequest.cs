using System.ComponentModel.DataAnnotations;

namespace Shared.Team;

public class NewTeamRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select a car.")]
    public int CarId { get; set; }

    [Required]
    public string Name { get; set; }
}

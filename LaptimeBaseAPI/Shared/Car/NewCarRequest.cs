using System.ComponentModel.DataAnnotations;

namespace Shared.Car;

public class NewCarRequest
{
    [Required]
    public string Class { get; set; }
}
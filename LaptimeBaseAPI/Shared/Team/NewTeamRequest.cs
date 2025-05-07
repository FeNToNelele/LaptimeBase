using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Team;

public class NewTeamRequest
{
    public int UserId { get; set; } //team owner's id
    public int CarId { get; set; }
    public string Name { get; set; }
}

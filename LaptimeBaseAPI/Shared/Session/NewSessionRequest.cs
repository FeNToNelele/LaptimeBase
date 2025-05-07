using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Session;

public class NewSessionRequest
{
    public DateTime HeldAt { get; set; }
    public int AmbientTemp { get; set; }
    public int TrackTemp { get; set; }
    public int TrackId { get; set; }
}

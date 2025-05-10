using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Laptime;

public class NewLapRequest
{
    public TimeSpan Laptime { get; set; }
    public int TeamId { get; set; }
    public int SessionId { get; set; }
}

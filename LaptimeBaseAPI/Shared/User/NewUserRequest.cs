using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.User;

public class NewUserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; } = false;
}

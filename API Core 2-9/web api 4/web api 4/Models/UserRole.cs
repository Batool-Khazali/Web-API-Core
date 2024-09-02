using System;
using System.Collections.Generic;

namespace web_api_4.Models;

public partial class UserRole
{
    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public virtual UsersWithHash User { get; set; } = null!;
}

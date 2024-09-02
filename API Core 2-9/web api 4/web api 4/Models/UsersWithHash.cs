using System;
using System.Collections.Generic;

namespace web_api_4.Models;

public partial class UsersWithHash
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Avatar { get; set; }

    public string? FacebookId { get; set; }

    public string? GoogleId { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
}

using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class UserSession
{
    public int SessionId { get; set; }

    public int? UserId { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? ExpireTime { get; set; }

    public string? Type { get; set; }

    public virtual User? User { get; set; }
}

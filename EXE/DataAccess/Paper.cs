using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class Paper
{
    public int PaperId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal? Money { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}

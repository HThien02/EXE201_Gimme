using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class Spring
{
    public int SpringId { get; set; }

    public string? Name { get; set; }

    public decimal? Money { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}

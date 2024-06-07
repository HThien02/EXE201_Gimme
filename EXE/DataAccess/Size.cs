using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class Size
{
    public int SizeId { get; set; }

    public string? Name { get; set; }

    public decimal? Money { get; set; }

    public string? Ratio { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}

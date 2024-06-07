using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class NumberOfPage
{
    public int NumberId { get; set; }

    public string? Description { get; set; }

    public int? Number { get; set; }

    public decimal? Money { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}

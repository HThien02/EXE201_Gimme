using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class Project
{
    public int ProjectId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public string? Image { get; set; }

    public int? SizeId { get; set; }

    public int? PaperId { get; set; }

    public int? SpringId { get; set; }

    public decimal? Total { get; set; }

    public int? NumberId { get; set; }

    public virtual NumberOfPage? Number { get; set; }

    public virtual Paper? Paper { get; set; }

    public virtual Size? Size { get; set; }

    public virtual Spring? Spring { get; set; }

    public virtual User? User { get; set; }
}

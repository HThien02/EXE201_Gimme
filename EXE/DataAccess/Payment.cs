using System;
using System.Collections.Generic;

namespace EXE.DataAccess;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? UserId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? PaymentMethod { get; set; }

    public int? Status { get; set; }

    public string? TransactionId { get; set; }

    public string? Description { get; set; }

    public virtual User? User { get; set; }
}

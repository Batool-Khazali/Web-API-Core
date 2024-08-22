using System;
using System.Collections.Generic;

namespace API_Core_21_8.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public string? OrderDate { get; set; }

    public virtual User? User { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WepAPICoreTasks.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductImage { get; set; }

    
    public virtual Category? Category { get; set; }
}

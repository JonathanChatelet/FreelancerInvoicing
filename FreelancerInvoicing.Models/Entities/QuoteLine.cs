using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Models.Entities;

public partial class QuoteLine
{
    [Key]
    public int QuoteLineId { get; set; }

    [Column("QuoteID")]
    public int QuoteId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? UnitPrice { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Total { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("QuoteLines")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("QuoteId")]
    [InverseProperty("QuoteLines")]
    public virtual Quote Quote { get; set; } = null!;
}

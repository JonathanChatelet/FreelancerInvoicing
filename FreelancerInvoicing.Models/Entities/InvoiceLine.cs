using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Models.Entities;

public partial class InvoiceLine
{
    [Key]
    public int InvoiceLineId { get; set; }

    [Column("InvoiceID")]
    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? UnitPrice { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Total { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceLines")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceLines")]
    public virtual Product Product { get; set; } = null!;
}

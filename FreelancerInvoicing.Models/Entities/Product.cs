using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Models.Entities;

public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(300)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? UnitPrice { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    [InverseProperty("Product")]
    public virtual ICollection<QuoteLine> QuoteLines { get; set; } = new List<QuoteLine>();

    [ForeignKey("UserId")]
    [InverseProperty("Products")]
    public virtual User User { get; set; } = null!;
}

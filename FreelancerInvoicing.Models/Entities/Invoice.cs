using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Models.Entities;

public partial class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public int UserId { get; set; }

    public int CustomerId { get; set; }

    public int? QuoteId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("TotalHT", TypeName = "decimal(10, 2)")]
    public decimal? TotalHt { get; set; }

    [Column("TotalTTC", TypeName = "decimal(10, 2)")]
    public decimal? TotalTtc { get; set; }

    public int Status { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Invoices")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    [ForeignKey("QuoteId")]
    [InverseProperty("Invoices")]
    public virtual Quote? Quote { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Invoices")]
    public virtual User User { get; set; } = null!;
}

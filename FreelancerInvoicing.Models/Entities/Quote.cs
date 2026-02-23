using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Models.Entities;

public partial class Quote
{
    [Key]
    public int QuoteId { get; set; }

    public int UserId { get; set; }

    public int CostumerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("TotalHT", TypeName = "decimal(10, 2)")]
    public decimal? TotalHt { get; set; }

    [Column("TotalTTC", TypeName = "decimal(10, 2)")]
    public decimal? TotalTtc { get; set; }

    public int Status { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Quotes")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Quote")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Quote")]
    public virtual ICollection<QuoteLine> QuoteLines { get; set; } = new List<QuoteLine>();

    [ForeignKey("UserId")]
    [InverseProperty("Quotes")]
    public virtual User User { get; set; } = null!;
}

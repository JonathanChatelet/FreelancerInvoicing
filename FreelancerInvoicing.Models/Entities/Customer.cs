using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Models.Entities;

[Index("Email", Name = "UQ__Customer__A9D10534CD372601", IsUnique = true)]
public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(80)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    public bool IsFrench { get; set; }

    public bool IsActive { get; set; }

    [StringLength(14)]
    public string? Siret { get; set; }

    [Column("VATNr")]
    [StringLength(14)]
    public string? Vatnr { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Customer")]
    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();

    [ForeignKey("UserId")]
    [InverseProperty("Customers")]
    public virtual User User { get; set; } = null!;
}

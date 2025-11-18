using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreelancerInvoicing.Models.Entities;

[Index("Email", Name = "UQ__Users__A9D10534A307F7BB", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(80)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? PasswordHash { get; set; }

    [StringLength(14)]
    public string? Siret { get; set; }

    [Column("IBAN")]
    [StringLength(34)]
    public string? Iban { get; set; }

    [Column("SWIFT")]
    [StringLength(11)]
    public string? Swift { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Costumer> Costumers { get; set; } = new List<Costumer>();

    [InverseProperty("User")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("User")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("User")]
    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}

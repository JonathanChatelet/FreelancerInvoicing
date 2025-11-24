using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FreelancerInvoicing.Models.Entities;

namespace FreelancerInvoicing.Repositories.Context;

public partial class FreelancerInvoicingDbContext : DbContext
{
    public FreelancerInvoicingDbContext()
    {
    }

    public FreelancerInvoicingDbContext(DbContextOptions<FreelancerInvoicingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Quote> Quotes { get; set; }

    public virtual DbSet<QuoteLine> QuoteLines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FreelancerInvoicingDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__8E5D69B06582650D");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsFrench).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__UserI__29572725");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAB5067447B5");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__Custom__37A5467C");

            entity.HasOne(d => d.Quote).WithMany(p => p.Invoices).HasConstraintName("FK__Invoices__QuoteI__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__UserId__36B12243");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.HasKey(e => e.InvoiceLineId).HasName("PK__InvoiceL__0D760AD9CC883AFA");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Invoi__412EB0B6");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Produ__4222D4EF");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD46698F5E");

            entity.HasOne(d => d.User).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__UserId__2E1BDC42");
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.QuoteId).HasName("PK__Quotes__AF9688C188782D3B");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Customer).WithMany(p => p.Quotes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quotes__Customer__31EC6D26");

            entity.HasOne(d => d.User).WithMany(p => p.Quotes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quotes__UserId__30F848ED");
        });

        modelBuilder.Entity<QuoteLine>(entity =>
        {
            entity.HasKey(e => e.QuoteLineId).HasName("PK__QuoteLin__89C6C90BE92AA975");

            entity.HasOne(d => d.Product).WithMany(p => p.QuoteLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuoteLine__Produ__3E52440B");

            entity.HasOne(d => d.Quote).WithMany(p => p.QuoteLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuoteLine__Quote__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C7DFE8381");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

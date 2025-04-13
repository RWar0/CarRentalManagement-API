using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class Invoice
{
    [Key]
    public int Id { get; set; }

    public int RentalId { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public DateOnly InvoiceDate { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("RentalId")]
    [InverseProperty("Invoices")]
    public virtual Rental Rental { get; set; } = null!;
}

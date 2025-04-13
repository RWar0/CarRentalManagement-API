using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class Rental
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int VehicleId { get; set; }

    public DateOnly BeginDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Rentals")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Rental")]
    public virtual ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();

    [InverseProperty("Rental")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("VehicleId")]
    [InverseProperty("Rentals")]
    public virtual Vehicle Vehicle { get; set; } = null!;
}

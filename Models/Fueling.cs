using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class Fueling
{
    [Key]
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public DateOnly FuelingDate { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal? Price { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [ForeignKey("VehicleId")]
    [InverseProperty("Fuelings")]
    public virtual Vehicle Vehicle { get; set; } = null!;
}

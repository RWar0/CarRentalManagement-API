using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class Vehicle
{
    [Key]
    public int Id { get; set; }

    [StringLength(64)]
    public string Brand { get; set; } = null!;

    [StringLength(128)]
    public string Model { get; set; } = null!;

    public int Production { get; set; }

    [Column("COLOR")]
    [StringLength(64)]
    public string Color { get; set; } = null!;

    public int VehicleCategyId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [InverseProperty("Vehicle")]
    public virtual ICollection<Fueling> Fuelings { get; set; } = new List<Fueling>();

    [InverseProperty("Vehicle")]
    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();

    [InverseProperty("Vehicle")]
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    [ForeignKey("VehicleCategyId")]
    [InverseProperty("Vehicles")]
    public virtual VehicleCategy VehicleCategy { get; set; } = null!;

    [InverseProperty("Vehicle")]
    public virtual ICollection<VehicleService> VehicleServices { get; set; } = new List<VehicleService>();
}

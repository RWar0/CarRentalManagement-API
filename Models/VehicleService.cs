using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class VehicleService
{
    [Key]
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public int ServiceId { get; set; }

    public DateOnly ServiceDate { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("VehicleServices")]
    public virtual Service Service { get; set; } = null!;

    [ForeignKey("VehicleId")]
    [InverseProperty("VehicleServices")]
    public virtual Vehicle Vehicle { get; set; } = null!;
}

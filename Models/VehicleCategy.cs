﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class VehicleCategy
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string Title { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [InverseProperty("VehicleCategy")]
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}

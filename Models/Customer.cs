using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class Customer
{
    [Key]
    public int Id { get; set; }

    [StringLength(64)]
    public string Firstname { get; set; } = null!;

    [StringLength(128)]
    public string Lastname { get; set; } = null!;

    [StringLength(11)]
    public string Pesel { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    [StringLength(128)]
    public string PlaceOfBirth { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    [InverseProperty("Customer")]
    public virtual ICollection<Warning> Warnings { get; set; } = new List<Warning>();
}

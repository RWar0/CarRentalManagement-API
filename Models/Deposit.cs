using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class Deposit
{
    [Key]
    public int Id { get; set; }

    public int RentalId { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [StringLength(64)]
    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [ForeignKey("RentalId")]
    [InverseProperty("Deposits")]
    public virtual Rental Rental { get; set; } = null!;
}

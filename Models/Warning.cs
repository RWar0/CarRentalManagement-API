using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Models;

public partial class Warning
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly WarningDate { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Warnings")]
    public virtual Customer Customer { get; set; } = null!;
}

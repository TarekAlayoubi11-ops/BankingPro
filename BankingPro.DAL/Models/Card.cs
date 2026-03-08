using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

[Index("CardNumber", Name = "UQ__Cards__A4E9FFE9A909E675", IsUnique = true)]
public partial class Card
{
    [Key]
    public int CardId { get; set; }

    [StringLength(16)]
    public string? CardNumber { get; set; }

    public int? AccountId { get; set; }

    [StringLength(20)]
    public string? Type { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Cards")]
    public virtual Account? Account { get; set; }
}

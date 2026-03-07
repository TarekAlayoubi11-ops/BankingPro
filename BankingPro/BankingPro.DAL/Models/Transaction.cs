using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

[Index("ReferenceNumber", Name = "UQ__Transact__C5ADBE4DD41AEDFD", IsUnique = true)]
public partial class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    [StringLength(20)]
    public string? ReferenceNumber { get; set; }

    public int? FromAccountId { get; set; }

    public int? ToAccountId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public int? TransactionTypeId { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("FromAccountId")]
    [InverseProperty("TransactionFromAccounts")]
    public virtual Account? FromAccount { get; set; }

    [ForeignKey("ToAccountId")]
    [InverseProperty("TransactionToAccounts")]
    public virtual Account? ToAccount { get; set; }

    [ForeignKey("TransactionTypeId")]
    [InverseProperty("Transactions")]
    public virtual TransactionType? TransactionType { get; set; }
}

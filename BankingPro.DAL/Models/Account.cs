using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

[Index("AccountNumber", Name = "UQ__Accounts__BE2ACD6F6CD66A4B", IsUnique = true)]
public partial class Account
{
    [Key]
    public int AccountId { get; set; }

    public int? CustomerId { get; set; }

    [StringLength(20)]
    public string? AccountNumber { get; set; }

    public int? AccountTypeId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Balance { get; set; }

    [StringLength(5)]
    public string? CurrencyCode { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OpenedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClosedAt { get; set; }

    public int? BranchId { get; set; }

    [ForeignKey("AccountTypeId")]
    [InverseProperty("Accounts")]
    public virtual AccountType? AccountType { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Accounts")]
    public virtual Branch? Branch { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    [ForeignKey("CurrencyCode")]
    [InverseProperty("Accounts")]
    public virtual Currency? CurrencyCodeNavigation { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Accounts")]
    public virtual Customer? Customer { get; set; }

    [InverseProperty("FromAccount")]
    public virtual ICollection<Transaction> TransactionFromAccounts { get; set; } = new List<Transaction>();

    [InverseProperty("ToAccount")]
    public virtual ICollection<Transaction> TransactionToAccounts { get; set; } = new List<Transaction>();
}

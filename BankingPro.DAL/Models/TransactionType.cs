using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

public partial class TransactionType
{
    [Key]
    public int TransactionTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("TransactionType")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

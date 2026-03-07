using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

public partial class Currency
{
    [Key]
    [StringLength(5)]
    public string CurrencyCode { get; set; } = null!;

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("CurrencyCodeNavigation")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}

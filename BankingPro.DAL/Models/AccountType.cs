using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

public partial class AccountType
{
    [Key]
    public int AccountTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("AccountType")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

public partial class Branch
{
    [Key]
    public int BranchId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}

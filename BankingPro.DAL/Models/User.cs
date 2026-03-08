using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingPro.DAL.Models;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(200)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LastLoginAt { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(300)]
    public string? RefreshTokenHash { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RefreshTokenExpiresAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RefreshTokenRevokedAt { get; set; }

    [ForeignKey("Role")]
    [InverseProperty("Users")]
    public virtual Role RoleNavigation { get; set; } = null!;
}

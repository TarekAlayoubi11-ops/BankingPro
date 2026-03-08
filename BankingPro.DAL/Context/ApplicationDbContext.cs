using System;
using System.Collections.Generic;
using BankingPro.DAL.Models;
using Microsoft.EntityFrameworkCore;
using BankingPro.DTOs;
namespace BankingPro.DAL.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }
    public virtual DbSet<User> Users { get; set; }


    public virtual DbSet<CardDTO> CardDTOs { get; set; }
    public virtual DbSet<TransactionDTO> TransactionDTOs { get; set; }
    public virtual DbSet<BranchDTO> BranchDTOs { get; set; }
    public virtual DbSet<AccountDTO> AccountDTOs { get; set; }
    public virtual DbSet<UserDTO> UserDTOs { get; set; }
    public virtual DbSet<CustomerDTO> CustomerDTOs { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardDTO>(u => u.HasNoKey().ToView(null));
        modelBuilder.Entity<AccountDTO>(u => u.HasNoKey().ToView(null));
        modelBuilder.Entity<BranchDTO>(u => u.HasNoKey().ToView(null));
        modelBuilder.Entity<UserDTO>(u => u.HasNoKey().ToView(null));
        modelBuilder.Entity<CustomerDTO>(u => u.HasNoKey().ToView(null));
        modelBuilder.Entity<TransactionDTO>(u => u.HasNoKey().ToView(null));

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5A666331025");

            entity.Property(e => e.Balance).HasDefaultValue(0m);
            entity.Property(e => e.OpenedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("Active");

            entity.HasOne(d => d.AccountType).WithMany(p => p.Accounts).HasConstraintName("FK__Accounts__Accoun__5165187F");

            entity.HasOne(d => d.Branch).WithMany(p => p.Accounts).HasConstraintName("FK__Accounts__Branch__5629CD9C");

            entity.HasOne(d => d.CurrencyCodeNavigation).WithMany(p => p.Accounts).HasConstraintName("FK__Accounts__Curren__534D60F1");

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts).HasConstraintName("FK__Accounts__Custom__5070F446");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.AccountTypeId).HasName("PK__AccountT__8F9585AFAC1CD5E2");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__Branches__A1682FC5B04EB6E7");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__Cards__55FECDAE09D8AB8E");

            entity.Property(e => e.Status).HasDefaultValue("Active");

            entity.HasOne(d => d.Account).WithMany(p => p.Cards).HasConstraintName("FK__Cards__AccountId__6754599E");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyCode).HasName("PK__Currenci__408426BE6D1216DB");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D85996FD3B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A7A40470D");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6BEFF77B77");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("Completed");

            entity.HasOne(d => d.FromAccount).WithMany(p => p.TransactionFromAccounts).HasConstraintName("FK__Transacti__FromA__5BE2A6F2");

            entity.HasOne(d => d.ToAccount).WithMany(p => p.TransactionToAccounts).HasConstraintName("FK__Transacti__ToAcc__5CD6CB2B");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions).HasConstraintName("FK__Transacti__Trans__5DCAEF64");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__Transact__20266D0B6A18C8C5");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CBF37029B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

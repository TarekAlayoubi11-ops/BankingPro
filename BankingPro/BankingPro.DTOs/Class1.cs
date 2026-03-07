namespace BankingPro.DTOs
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountTypeName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string CurrencyName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string BranchName { get; set; } = string.Empty;
    }
    public class CreateAccountDTO
    {
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public int AccountTypeId { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public int BranchId { get; set; }
    }
    public class UpdateAccountDTO
    {
        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int BranchId { get; set; }
    }

    public class UserDTO
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class CreateUserDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
    public class UpdateUserDTO
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
    }
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? RiskLevel { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    public class CreateCustomerDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? RiskLevel { get; set; } = string.Empty;
    }
    public class UpdateCustomerDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? RiskLevel { get; set; } = string.Empty;
    }
    public class BranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
    public class CreateBranchDTO
    {
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
    public class UpdateBranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
    public class CardDTO
    {
        public int Id { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
    public class CreateCardDTO
    {
        public string CardNumber { get; set; } = string.Empty;
        public int AccountId { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
    public class UpdateCardDTO
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string? TransactionType { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ReferenceNumber { get; set; }
    }
    public class CreateTransactionDTO
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeId { get; set; }
    }
    public class UpdateTransactionStatusDTO
    {
        public int Id { get; set; }
        public string? Status { get; set; }
    }
}

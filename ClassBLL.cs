
using BankingPro.DAL;
using BankingPro.DTO;
using BCrypt.Net;

namespace BankingPro.BLL
{
    public static class UserBll
    {
        public static OperationResult<int> CreateUser(CreateUserDTO dto)
        {
            var result = new OperationResult<int>();
            try
            {
                dto.Role = dto.Role!.ToLower();
                if (string.IsNullOrWhiteSpace(dto.Username) ||
                    string.IsNullOrWhiteSpace(dto.Password) ||
                    (dto.Role != "admin" && dto.Role != "user" && dto.Role != "employee"))
                {
                    result.Success = false;
                    result.Message = "Invalid input data.";
                    return result;
                }
                dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                int code = UserDal.CreateUser(dto);
                switch (code)
                {
                    case > 0:
                        result.Success = true;
                        result.Message = "User created successfully.";
                        result.Data = code;
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "Unable to create user. Please check your information.";
                        break;
                    default:
                        result.Success = false; result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> UpdateUser(UpdateUserDTO dto)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (dto.UserId <= 0 || string.IsNullOrWhiteSpace(dto.Username) ||
                    string.IsNullOrWhiteSpace(dto.Role))
                {
                    result.Success = false;
                    result.Message = "Invalid input data.";
                    return result;
                }
                int code = UserDal.UpdateUser(dto);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Message = "User updated successfully.";
                        result.Data = true;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "User not found.";
                        break;
                    case -4:
                        result.Success = false;
                        result.Message = "Unable to updated user. Please check your information.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> DeleteUser(int userId)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (userId <= 0)
                {
                    result.Success = false;
                    result.Message = "Invalid user id.";
                    return result;
                }
                int code = UserDal.DeleteUser(userId);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Message = "User deleted successfully.";
                        result.Data = true;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "User not found.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<UserDTO> GetUserById(int userId)
        {
            if (userId <= 0)
            {
                return new OperationResult<UserDTO>
                {
                    Success = false,
                    Message = "Invalid user id."
                };
            }
            return UserDal.GetUserById(userId);
        }
        public static OperationResult<List<UserDTO>> GetAllUsers()
        {
            return UserDal.GetAllUsers();
        }
    }
    public static class CustomerBll
    {
        public static OperationResult<int> CreateCustomer(CreateCustomerDTO dto)
        {
            var result = new OperationResult<int>();
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                    string.IsNullOrWhiteSpace(dto.LastName) ||
                    string.IsNullOrWhiteSpace(dto.Phone) ||
                    string.IsNullOrWhiteSpace(dto.Address) ||
                    string.IsNullOrWhiteSpace(dto.RiskLevel) ||
                    string.IsNullOrWhiteSpace(dto.Email))
                {
                    result.Success = false;
                    result.Message = "Invalid input data.";
                    return result;
                }
                int code = CustomerDal.CreateCustomer(dto);
                switch (code)
                {
                    case > 0:
                        result.Success = true;
                        result.Message = "Customer created successfully.";
                        result.Data = code;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Email already exists";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> UpdateCustomer(UpdateCustomerDTO dto)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (dto.CustomerId <= 0 || string.IsNullOrWhiteSpace(dto.FirstName)
                    || string.IsNullOrWhiteSpace(dto.LastName) || string.IsNullOrWhiteSpace(dto.Phone)
                    || string.IsNullOrWhiteSpace(dto.Address) || string.IsNullOrWhiteSpace(dto.RiskLevel) ||
                    string.IsNullOrWhiteSpace(dto.Email))
                {
                    result.Success = false;
                    result.Message = "Invalid input data.";
                    return result;
                }
                int code = CustomerDal.UpdateCustomer(dto);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Message = "Customer updated successfully.";
                        result.Data = true;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Customer not found.";
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "Email already exists.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> DeleteCustomer(int customerId)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (customerId <= 0)
                {
                    result.Success = false;
                    result.Message = "Invalid customer id.";
                    return result;
                }
                int code = CustomerDal.DeleteCustomer(customerId);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Message = "customer deleted successfully.";
                        result.Data = true;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "customer not found.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<CustomerDTO> GetCustomerById(int customerId)
        {
            return CustomerDal.GetCustomerById(customerId);
        }
        public static OperationResult<List<CustomerDTO>> GetAllActiveCustomers()
        {
            return CustomerDal.GetAllActiveCustomers();
        }
    }
    public static class BranchBll
    {
        public static OperationResult<int> CreateBranch(CreateBranchDTO dto)
        {
            var result = new OperationResult<int>();
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name) ||
                    string.IsNullOrWhiteSpace(dto.City))
                {
                    result.Success = false;
                    result.Message = "Invalid input data.";
                    return result;
                }
                int code = BranchDal.CreateBranch(dto);
                switch (code)
                {
                    case > 0:
                        result.Success = true;
                        result.Message = "Branch created successfully.";
                        result.Data = code;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Branch already exists in this city.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> UpdateBranch(UpdateBranchDTO dto)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (dto.Id <= 0 || string.IsNullOrWhiteSpace(dto.Name) ||
                    string.IsNullOrWhiteSpace(dto.City))
                {
                    result.Success = false;
                    result.Message = "Invalid input data.";
                    return result;
                }
                int code = BranchDal.UpdateBranch(dto);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Message = "Branch updated successfully.";
                        result.Data = true;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Branch not found.";
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "Branch already exists in this city.";
                        break;
                    case -3:
                        result.Success = false;
                        result.Message = "Invalid branch id.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> DeleteBranch(int id)
        {
            var result = new OperationResult<bool>();
            try
            {
                if (id <= 0)
                {
                    result.Success = false;
                    result.Message = "Invalid branch id.";
                    return result;
                }
                int code = BranchDal.DeleteBranch(id);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Message = "Branch deleted successfully.";
                        result.Data = true;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Branch not found.";
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "branch Already Deleted.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<BranchDTO> GetBranchById(int id)
        {
            if (id <= 0)
            {
                return new OperationResult<BranchDTO>
                {
                    Success = false,
                    Message = "Invalid branch id."
                };
            }
            return
                BranchDal.GetBranchById(id);
        }
        public static OperationResult<List<BranchDTO>> GetAllActiveBranches()
        {
            return BranchDal.GetAllActiveBranches();
        }
    }
    public static class AccountBll
    {
        public static OperationResult<int> CreateAccount(CreateAccountDTO dto)
        {
            var result = new OperationResult<int>();
            if (dto.CustomerId <= 0 || dto.BranchId <= 0 || dto.AccountTypeId <= 0 ||
                string.IsNullOrWhiteSpace(dto.CurrencyCode) ||
                string.IsNullOrWhiteSpace(dto.AccountNumber))
            {
                result.Success = false;
                result.Message = "Invalid input data.";
                return result;
            }
            int code = AccountDal.CreateAccount(dto);
            switch (code)
            {
                case > 0:
                    result.Success = true;
                    result.Message = "Account created successfully.";
                    result.Data = code;
                    break;
                case -1:
                    result.Success = false;
                    result.Message = "Customer not found.";
                    break;
                case -2:
                    result.Success = false;
                    result.Message = "Branch not found.";
                    break;
                case -3:
                    result.Success = false;
                    result.Message = "account type not found.";
                    break;
                case -4:
                    result.Success = false;
                    result.Message = "Account number already exists.";
                    break;
                case -5:
                    result.Success = false;
                    result.Message = "currency not found.";
                    break;
                default:
                    result.Success = false;
                    result.Message = "System error occurred.";
                    break;
            }
            return result;
        }
        public static OperationResult<bool> UpdateAccount(UpdateAccountDTO dto)
        {
            var result = new OperationResult<bool>();
            if (dto.Id <= 0 || dto.AccountTypeId <= 0 || dto.BranchId <= 0 ||
                string.IsNullOrWhiteSpace(dto.Status) ||
                (dto.Status != "Closed" && dto.Status != "Active")
                || string.IsNullOrWhiteSpace(dto.CurrencyCode))
            {
                result.Success = false;
                result.Message = "Invalid input data.";
                return result;
            }
            int code = AccountDal.UpdateAccount(dto);
            switch (code)
            {
                case 1:
                    result.Success = true;
                    result.Data = true;
                    result.Message = "Account updated successfully.";
                    break;
                case -1:
                    result.Success = false;
                    result.Message = "Account not found.";
                    break;
                case -2:
                    result.Success = false;
                    result.Message = "account type not found.";
                    break;
                case -3:
                    result.Success = false;
                    result.Message = "branch not found.";
                    break;
                case -4:
                    result.Success = false;
                    result.Message = "currency not found.";
                    break;
                default:
                    result.Success = false;
                    result.Message = "System error occurred.";
                    break;
            }
            return result;
        }
        public static OperationResult<bool> CloseAccount(int id)
        {
            var result = new OperationResult<bool>();
            if (id <= 0)
            {
                result.Success = false;
                result.Message = "Invalid Account id.";
                return result;
            }
            int code = AccountDal.CloseAccount(id);
            switch (code)
            {
                case 1:
                    result.Success = true;
                    result.Data = true;
                    result.Message = "Account closed successfully.";
                    break;
                case -1:
                    result.Success = false;
                    result.Message = "Account not found.";
                    break;
                case -2:
                    result.Success = false;
                    result.Message = "Account balance must be zero.";
                    break;
                default:
                    result.Success = false;
                    result.Message = "System error occurred.";
                    break;
            }
            return result;
        }
        public static OperationResult<AccountDTO> GetAccountById(int id)
        {
            return AccountDal.GetAccountById(id);
        }
        public static OperationResult<List<AccountDTO>> GetAllActiveAccounts()
        {
            return AccountDal.GetAllActiveAccounts();
        }
    }
    public static class TransactionBll
    {
        public static OperationResult<bool> Reverse(int id)
        {
            var result = new OperationResult<bool>();
            try
            {
                int code = TransactionDal.ReverseTransaction(id);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Data = true;
                        result.Message = "Transaction reversed successfully.";
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Transaction not found.";
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "Transaction already reversed.";
                        break;
                    case -4:
                    case -3:
                        result.Success = false;
                        result.Message = "Account not active.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<int> Create(CreateTransactionDTO dto)
        {
            var result = new OperationResult<int>();
            try
            {
                int code = TransactionDal.CreateTransaction(dto);
                switch (code)
                {
                    case > 0:
                        result.Success = true;
                        result.Data = code;
                        result.Message = "Transaction completed successfully.";
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Invalid transaction type.";
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "Invalid amount.";
                        break;
                    case -3:
                        result.Success = false;
                        result.Message = "Source account is required.";
                        break;
                    case -4:
                        result.Success = false;
                        result.Message = "Destination account is required.";
                        break;
                    case -5:
                        result.Success = false;
                        result.Message = "Cannot transfer to the same account.";
                        break;
                    case -6:
                        result.Success = false;
                        result.Message = "Source account not found.";
                        break;
                    case -7:
                        result.Success = false;
                        result.Message = "Source account is not active.";
                        break;
                    case -8:
                        result.Success = false;
                        result.Message = "Destination account not found.";
                        break;
                    case -9:
                        result.Success = false;
                        result.Message = "Destination account is not active.";
                        break;
                    case -10:
                        result.Success = false;
                        result.Message = "Insufficient balance.";
                        break;
                    case -99:
                        result.Success = false;
                        result.Message = "Database error occurred.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "Unknown error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<TransactionDTO> GetById(int id)
        {
            if (id <= 0)
            {
                return new OperationResult<TransactionDTO>
                {
                    Success = false,
                    Message = "Invalid Transaction id."
                };
            }
            return TransactionDal.GetById(id);
        }
        public static OperationResult<List<TransactionDTO>> GetAll()
        {
            return TransactionDal.GetAll();
        }
        public static OperationResult<bool> UpdateStatus(UpdateTransactionStatusDTO dto)
        {
            var result = new OperationResult<bool>();
            if (dto.Id <= 0 || (dto.Status != "'Completed" && dto.Status != "Reversed"))
            {
                result.Success = false;
                result.Message = "Invalid input data.";
                return result;
            }
            try
            {
                int code = TransactionDal.UpdateStatus(dto);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Data = true; result.Message = "Transaction status updated successfully.";
                        break;
                    case -1:
                        result.Success = false; result.Message = "Transaction not found.";
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "Invalid status.";
                        break;
                    case -3:
                        result.Success = false;
                        result.Message = "Transaction already reversed.";
                        break;
                    default:
                        result.Success = false; result.Message = "System error occurred."; break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
    }
    public static class CardBll
    {
        public static OperationResult<int> CreateCard(CreateCardDTO dto)
        {
            var result = new OperationResult<int>();
            if ((dto.Type != "Debit" && dto.Type != "Credit" && dto.Type != "ATM") ||
            string.IsNullOrWhiteSpace(dto.CardNumber) ||
            dto.AccountId <= 0)
            {
                result.Success = false;
                result.Message = "Invalid input data.";
                return result;
            }
            try
            {
                int code = CardDal.CreateCard(dto);
                switch (code)
                {
                    case > 0:
                        result.Success = true;
                        result.Message = "Card created successfully.";
                        result.Data = code;
                        break;
                    case -1:
                        result.Message = "Account not found.";
                        break;
                    case -2:
                        result.Message = "Account not active.";
                        break;
                    case -3:
                        result.Message = "Card number already exists.";
                        break;
                    case -4:
                        result.Message = "Invalid expiry date.";
                        break;
                    case -5:
                        result.Message = "Debit already exists.";
                        break;
                    case -6:
                        result.Message = "Credit already exists.";
                        break;
                    default:
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> UpdateCard(UpdateCardDTO dto)
        {
            var result = new OperationResult<bool>();
            if (dto.Id <= 0 || (dto.Type != "Debit" && dto.Type != "Credit" && dto.Type != "ATM")
                || (dto.Status != "Active" && dto.Type != "Blocked" && dto.Type != "Expired"))
            {
                result.Success = false;
                result.Message = "Invalid input data.";
                return result;
            }
            try
            {
                int code = CardDal.UpdateCard(dto);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Message = "Card updated successfully.";
                        result.Data = true;
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Card not found.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred.";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<bool> BlockCard(int id)
        {
            var result = new OperationResult<bool>(); if (id <= 0) { result.Success = false; result.Message = "Invalid card id."; return result; }
            try
            {
                int code = CardDal.BlockCard(id);
                switch (code)
                {
                    case 1:
                        result.Success = true;
                        result.Data = true; result.Message = "Card blocked successfully.";
                        break;
                    case -1:
                        result.Success = false;
                        result.Message = "Card not found.";
                        break;
                    case -2:
                        result.Success = false;
                        result.Message = "Card Already Blocked.";
                        break;
                    default:
                        result.Success = false;
                        result.Message = "System error occurred."; break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false; result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<CardDTO> GetCardById(int id)
        {
            return CardDal.GetCardById(id);
        }
        public static OperationResult<List<CardDTO>> GetAllActiveCards()
        {
            return CardDal.GetAllActiveCards();
        }
    }
}



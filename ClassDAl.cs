

using BankingPro.DTO;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using BankingPro.DAL.context;
namespace BankingPro.DAL
{
    public static class UserDal
    {
        private static readonly ApplicationDbContext _context = new ApplicationDbContext();
        public static int CreateUser(CreateUserDTO user)
        {
            try
            {
                var resultParam = new SqlParameter
                {
                    ParameterName = "@Result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                _context.Database.ExecuteSqlRaw("exec @Result = sp_CreateUser @Username, @Password, @Role",
               new SqlParameter("@Username", user.Username),
               new SqlParameter("@Password", user.Password),
               new SqlParameter("@Role", user.Role),
               resultParam);
                return (int)resultParam.Value!;
            }
            catch
            {
                return -99;
            }
        }
        public static int UpdateUser(UpdateUserDTO user)
        {
            try
            {
                var resultParam = new SqlParameter
                {
                    ParameterName = "@Result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                _context.Database.ExecuteSqlRaw("exec @Result = sp_UpdateUser @UserId, @Username, @Password, @Role",
                    new SqlParameter("@UserId", user.UserId),
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Password", DBNull.Value),
                    new SqlParameter("@Role", user.Role),
                    resultParam);
                return
                    (int)resultParam.Value!;
            }
            catch
            {
                return -99;
            }
        }
        public static int DeleteUser(int userId)
        {
            try
            {
                var resultParam = new SqlParameter
                {
                    ParameterName = "@Result",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                }; _context.Database.ExecuteSqlRaw("exec @Result = sp_DeleteUser @UserId",
                    new SqlParameter("@UserId", userId), resultParam);
                return (int)resultParam.Value!;
            }
            catch { return -99; }
        }
        public static OperationResult<UserDTO> GetUserById(int userId)
        {
            var result = new OperationResult<UserDTO>();
            try
            {
                var user = _context.UserDTOs.FromSqlRaw("sp_GetUserById @UserId",
                    new SqlParameter("@UserId", userId)).ToList();
                if (user != null)
                {
                    result.Success = true;
                    result.Message = "User found successfully.";
                    result.Data = user.FirstOrDefault();
                }
                else
                {
                    result.Success = false;
                    result.Message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<List<UserDTO>> GetAllUsers() { var result = new OperationResult<List<UserDTO>>() { Data = new List<UserDTO>() }; try { var users = _context.UserDTOs.FromSqlRaw("sp_GetAllUsers").ToList(); if (users.Count > 0) { result.Success = true; result.Message = "Users retrieved successfully."; result.Data = users; } else { result.Success = false; result.Message = "No users found."; } } catch (Exception ex) { result.Success = false; result.Message = $"System error: {ex.Message}"; } return result; }
    }
    public static class CustomerDal
    {
        private static readonly ApplicationDbContext _context = new ApplicationDbContext();
        public static int CreateCustomer(CreateCustomerDTO dto)
        {
            var paramNewId = new SqlParameter
            {
                ParameterName = "@NewCustomerId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            var result = _context.Database.ExecuteSqlRaw("EXEC NewCustomerId= sp_CreateCustomer @FirstName, @LastName," +
                " @DateOfBirth, @Email, @Phone, @Address, @RiskLevel",
                new SqlParameter("@FirstName", dto.FirstName), new SqlParameter("@LastName", dto.LastName),
                new SqlParameter("@DateOfBirth", dto.DateOfBirth),
                new SqlParameter("@Email", (object?)dto.Email ?? DBNull.Value),
                new SqlParameter("@Phone", (object?)dto.Phone ?? DBNull.Value),
                new SqlParameter("@Address", (object?)dto.Address ?? DBNull.Value),
                new SqlParameter("@RiskLevel", (object?)dto.RiskLevel ?? DBNull.Value),
                paramNewId
                );
            return (int)(paramNewId.Value ?? -1);

        }
        public static int UpdateCustomer(UpdateCustomerDTO dto)
        {
            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            _context.Database.ExecuteSqlRaw(" @Result = sp_UpdateCustomer @CustomerId, @FirstName, @LastName," +
                " @DateOfBirth, @Email, @Phone, @Address, @RiskLevel",
                new SqlParameter("@CustomerId", dto.CustomerId), new SqlParameter("@FirstName", dto.FirstName),
                new SqlParameter("@LastName", dto.LastName), new SqlParameter("@DateOfBirth", dto.DateOfBirth),
                new SqlParameter("@Email", (object?)dto.Email ?? DBNull.Value),
                new SqlParameter("@Phone", (object?)dto.Phone ?? DBNull.Value),
                new SqlParameter("@Address", (object?)dto.Address ?? DBNull.Value),
                new SqlParameter("@RiskLevel", (object?)dto.RiskLevel ?? DBNull.Value),
                resultParam);
            return (int)resultParam.Value!;
        }
        public static int DeleteCustomer(int customerId)
        {
            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            _context.Database.ExecuteSqlRaw(" @Result = sp_DeleteCustomer @CustomerId",
                new SqlParameter("@CustomerId", customerId),
                resultParam);
            return (int)resultParam.Value;
        }
        public static OperationResult<CustomerDTO> GetCustomerById(int customerId)
        {
            var result = new OperationResult<CustomerDTO>();
            try
            {
                var customer = _context.CustomerDTOs.FromSqlRaw("sp_GetCustomerById @CustomerId",
                    new SqlParameter("@CustomerId", customerId)).ToList();
                if (customer != null)
                {
                    result.Success = true;
                    result.Message = "customer found successfully.";
                    result.Data = customer.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<List<CustomerDTO>> GetAllActiveCustomers()
        {
            var result = new OperationResult<List<CustomerDTO>>()
            {
                Data = new List<CustomerDTO>()
            };
            try
            {
                var customers = _context.CustomerDTOs.FromSqlRaw(" sp_GetAllActiveCustomers").ToList();
                if (customers.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Customers retrieved successfully.";
                    result.Data = customers;
                }
                else
                {
                    result.Success = false;
                    result.Message = "No customers found.";
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
    public static class BranchDal
    {
        private static readonly ApplicationDbContext _context = new ApplicationDbContext();
        public static int CreateBranch(CreateBranchDTO dto)
        {
            var paramNewId = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            _context.Database.ExecuteSqlRaw("EXEC @Result= sp_CreateBranch @Name, @City",
                new SqlParameter("@Name", dto.Name), new SqlParameter("@City", dto.City),
                paramNewId);
            return (int)(paramNewId.Value ?? -99);
        }
        public static int UpdateBranch(UpdateBranchDTO dto)
        {
            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            _context.Database.ExecuteSqlRaw("exec @Result= sp_UpdateBranch @Id, @Name, @City",
                new SqlParameter("@Id", dto.Id),
                new SqlParameter("@Name", dto.Name),
                new SqlParameter("@City", dto.City),
                resultParam);
            return (int)(resultParam.Value ?? -99);
        }
        public static int DeleteBranch(int id)
        {
            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            _context.Database.ExecuteSqlRaw("exec @Result= sp_DeleteBranch @Id", new SqlParameter("@Id", id),
                resultParam);
            return (int)(resultParam.Value ?? -99);
        }
        public static OperationResult<BranchDTO> GetBranchById(int id)
        {
            var result = new OperationResult<BranchDTO>();
            try
            {
                var branch = _context.BranchDTOs.FromSqlRaw("sp_GetBranchById @Id",
                    new SqlParameter("@Id", id)).ToList();
                if (branch != null && branch.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Branch found successfully.";
                    result.Data = branch.FirstOrDefault();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Branch not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"System error: {ex.Message}";
            }
            return result;
        }
        public static OperationResult<List<BranchDTO>> GetAllActiveBranches()
        {
            var result = new OperationResult<List<BranchDTO>>()
            {
                Data = new List<BranchDTO>()
            };
            try
            {
                var branches = _context.BranchDTOs.FromSqlRaw("sp_GetAllBranches").ToList();
                if (branches.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Branches retrieved successfully.";
                    result.Data = branches;
                }
                else
                {
                    result.Success = false;
                    result.Message = "No branches found.";
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
    public static class AccountDal
    {
        private static readonly ApplicationDbContext _context = new ApplicationDbContext();
        public static int CreateAccount(CreateAccountDTO dto)
        {
            try
            {
                var returnParam = new SqlParameter
                {
                    ParameterName = "@ReturnVal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue
                };
                _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_CreateAccount @CustomerId, @AccountNumber, @AccountTypeId," +
                    " @Balance, @CurrencyCode, @BranchId",
                    returnParam,
                    new SqlParameter("@CustomerId", dto.CustomerId),
                    new SqlParameter("@AccountNumber", dto.AccountNumber),
                    new SqlParameter("@AccountTypeId", dto.AccountTypeId),
                    new SqlParameter("@Balance", dto.Balance),
                    new SqlParameter("@CurrencyCode", dto.CurrencyCode),
                    new SqlParameter("@BranchId", dto.BranchId));
                return (int)(returnParam.Value);
            }
            catch
            {
                return -99;
            }
        }
        public static int UpdateAccount(UpdateAccountDTO dto)
        {
            try
            {
                var returnParam = new SqlParameter
                {
                    ParameterName = "@ReturnVal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue
                };
                _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_UpdateAccount @Id, @AccountTypeId," +
                    " @CurrencyCode, @Status, @BranchId",
                    returnParam,
                    new SqlParameter("@Id", dto.Id), new SqlParameter("@AccountTypeId",
                    dto.AccountTypeId), new SqlParameter("@CurrencyCode", dto.CurrencyCode),
                    new SqlParameter("@Status", dto.Status), new SqlParameter("@BranchId", dto.BranchId));
                return (int)(returnParam.Value);
            }
            catch
            {
                return -99;
            }
        }
        public static int CloseAccount(int id)
        {
            try
            {
                var returnParam = new SqlParameter
                {
                    ParameterName = "@ReturnVal",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.ReturnValue
                };
                _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_CloseAccount @Id", returnParam,
                    new SqlParameter("@Id", id));
                return (int)returnParam.Value;
            }
            catch
            {
                return -99;
            }
        }
        public static OperationResult<AccountDTO> GetAccountById(int id)
        {
            var result = new OperationResult<AccountDTO>();
            try
            {
                var account = _context.AccountDTOs.FromSqlRaw("sp_GetAccountById @Id",
                    new SqlParameter("@Id", id)).ToList();
                if (account.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Account found successfully.";
                    result.Data = account.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static OperationResult<List<AccountDTO>> GetAllActiveAccounts()
        {
            var result = new OperationResult<List<AccountDTO>>()
            {
                Data = new List<AccountDTO>()
            };
            try
            {
                var accounts = _context.AccountDTOs.FromSqlRaw("sp_GetAllActiveAccounts").ToList();
                if (accounts.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Accounts retrieved successfully.";
                    result.Data = accounts;
                }
                else
                {
                    result.Success = false;
                    result.Message = "No accounts found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
    public static class CardDal
    {
        private static readonly ApplicationDbContext _context = new ApplicationDbContext();
        public static int CreateCard(CreateCardDTO dto)
        {
            var returnParam = new SqlParameter
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue
            };
            _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_CreateCard @CardNumber, @AccountId, @Type," +
                " @ExpiryDate", returnParam, new SqlParameter("@CardNumber", dto.CardNumber),
                new SqlParameter("@AccountId", dto.AccountId), new SqlParameter("@Type", dto.Type),
                new SqlParameter("@ExpiryDate", dto.ExpiryDate));
            return (int)(returnParam.Value ?? -99);
        }
        public static int UpdateCard(UpdateCardDTO dto)
        {
            var returnParam = new SqlParameter
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue
            };
            _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_UpdateCard @Id, @Type, @Status, @ExpiryDate",
                returnParam, new SqlParameter("@Id", dto.Id), new SqlParameter("@Type", dto.Type),
                new SqlParameter("@Status", dto.Status), new SqlParameter("@ExpiryDate", dto.ExpiryDate));
            return (int)(returnParam.Value ?? -99);
        }
        public static int BlockCard(int id)
        {
            var returnParam = new SqlParameter
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue
            };
            _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_BlockCard @Id", returnParam,
                new SqlParameter("@Id", id));
            return (int)(returnParam.Value ?? -99);
        }
        public static OperationResult<CardDTO> GetCardById(int id)
        {
            var result = new OperationResult<CardDTO>();
            try
            {
                var card = _context.CardDTOs.FromSqlRaw("sp_GetCardById @Id",
                    new SqlParameter("@Id", id)).ToList();
                if (card.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Card found successfully.";
                    result.Data = card.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static OperationResult<List<CardDTO>> GetAllActiveCards()
        {
            var result = new OperationResult<List<CardDTO>>()
            {
                Data = new List<CardDTO>()
            };
            try
            {
                var cards = _context.CardDTOs.FromSqlRaw("sp_GetAllActiveCards").ToList();
                if (cards.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Cards retrieved successfully.";
                    result.Data = cards;
                }
                else
                {
                    result.Success = false;
                    result.Message = "No cards found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
    public static class TransactionDal
    {
        private static readonly ApplicationDbContext _context = new ApplicationDbContext(); 
        public static int ReverseTransaction(int id)
        {
            var returnParam = new SqlParameter
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue
            };
            _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_ReverseTransaction @Id",
                returnParam,
                new SqlParameter("@Id", id));
            return (int)(returnParam.Value ?? -99);
        }
        public static int CreateTransaction(CreateTransactionDTO dto)
        {
            var returnParam = new SqlParameter
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue
            };
            _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_CreateTransaction @FromAccountId," +
                " @ToAccountId, @Amount, @TransactionTypeId",
                returnParam,
                new SqlParameter("@FromAccountId", (object?)dto.FromAccountId ?? DBNull.Value),
                new SqlParameter("@ToAccountId", (object?)dto.ToAccountId ?? DBNull.Value),
                new SqlParameter("@Amount", dto.Amount),
                new SqlParameter("@TransactionTypeId", dto.TransactionTypeId));
            return (int)(returnParam.Value ?? -99);
        }
        public static OperationResult<TransactionDTO> GetById(int id)
        {
            var result = new OperationResult<TransactionDTO>();
            try
            {
                var trx = _context.TransactionDTOs.FromSqlRaw("sp_GetTransactionById @Id",
                    new SqlParameter("@Id", id)).ToList();
                if (trx.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Transaction found successfully.";
                    result.Data = trx.FirstOrDefault();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Transaction not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static OperationResult<List<TransactionDTO>> GetAll()
        {
            var result = new OperationResult<List<TransactionDTO>>()
            {
                Data = new List<TransactionDTO>()
            };
            try
            {
                var list = _context.TransactionDTOs.FromSqlRaw("sp_GetAllTransactions").ToList();
                if (list.Count > 0)
                {
                    result.Success = true;
                    result.Message = "Transactions retrieved successfully.";
                    result.Data = list;
                }
                else
                {
                    result.Success = false;
                    result.Message = "No transactions found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static int UpdateStatus(UpdateTransactionStatusDTO dto)
        {
            var returnParam = new SqlParameter
            {
                ParameterName = "@ReturnVal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.ReturnValue
            };
            _context.Database.ExecuteSqlRaw("EXEC @ReturnVal = sp_UpdateTransactionStatus @Id, @Status",
                returnParam,
                new SqlParameter("@Id", dto.Id),
                new SqlParameter("@Status", dto.Status));
            return (int)(returnParam.Value ?? -99);
        }
    }
}
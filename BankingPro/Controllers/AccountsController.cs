using BankingPro.BLL;
using BankingPro.DAL.Context;
using BankingPro.DAL.Models;
using BankingPro.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingPro.Controllers
{
    [Authorize]
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public IActionResult Create(CreateAccountDTO dto)
        {
            var result = AccountBll.CreateAccount(dto);

            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest();
                if (result.Message == "Customer not found.")
                    return NotFound();

                if (result.Message == "Branch not found.")
                    return NotFound();
                if (result.Message == "account type not found.")
                    return NotFound();

                if (result.Message == "Account number already exists.")
                    return Conflict();

                if (result.Message == "currency not found.")
                    return NotFound();
                return StatusCode(500);
            }

            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPut]
        public IActionResult Update(UpdateAccountDTO dto)
        {
            var result = AccountBll.UpdateAccount(dto);

            if (!result.Success)
            {
                if (result.Message == "Account not found.")
                    return NotFound();
                if (result.Message == "Invalid input data.")
                    return BadRequest();
                if (result.Message == "account type not found.")
                    return NotFound();
                if (result.Message == "branch not found.")
                    return NotFound();
                if (result.Message == "currency not found.")
                    return NotFound();

                return StatusCode(500);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("{id}")]
        public IActionResult Close(int id)
        {
            var result = AccountBll.CloseAccount(id);

            if (!result.Success)
            {
                if (result.Message == "Invalid Account id.")
                    return BadRequest();
                if (result.Message == "Account not found.")
                    return NotFound();
                if (result.Message == "Account balance must be zero.")
                    return Conflict();

                return BadRequest();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetByIdAsync(int id, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest("Invalid Account id.");

            ApplicationDbContext Context = new ApplicationDbContext();
            var user = Context.Accounts.FirstOrDefault(u => u.AccountId == id);

            if (user == null)
                return NotFound("Account not found.");

            var authResult = await authorizationService.AuthorizeAsync(
                User,
                id,
                "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();

            {
                var result = AccountBll.GetAccountById(id);

                if (!result.Success)
                {
                    if (result.Message == "Invalid Account id.")
                        return BadRequest();

                    return StatusCode(500, result);
                }
                return Ok(result.Data);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<AccountDTO> GetAllActive()
        {
            var result = AccountBll.GetAllActiveAccounts();

            if (!result.Success)
                if (result.Message == "No accounts found.")
                    return NotFound();
                else
                    return StatusCode(500, result);

            return Ok(result.Data);

        }
    }
}

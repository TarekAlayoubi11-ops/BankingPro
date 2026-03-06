using BankingPro.BLL;
using BankingPro.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingPro.Controllers
{
    [Authorize]
    [Route("api/Transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPut("reverse/{id}")]
        public IActionResult Reverse(int id)
        {
            var result = TransactionBll.Reverse(id);

            if (!result.Success)
            {
                if (result.Message == "Transaction not found.")
                    return NotFound(result);

                if (result.Message == "Transaction already reversed.")
                    return Conflict(result);

                if (result.Message == "Account not active.")
                    return Conflict(result);

                return StatusCode(500, result);
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateTransactionDTO dto)
        {
            var result = TransactionBll.Create(dto);

            if (!result.Success)
            {
                if (result.Message == "Source account not found." ||
                    result.Message == "Destination account not found.")
                {
                    return NotFound(result);
                }

                if (result.Message == "Cannot transfer to the same account." ||
                    result.Message == "Source account is not active." ||
                    result.Message == "Destination account is not active." ||
                    result.Message == "Insufficient balance.")
                {
                    return Conflict(result);
                }

                if (result.Message == "Invalid transaction type." ||
                    result.Message == "Invalid amount." ||
                    result.Message == "Source account is required." ||
                    result.Message == "Destination account is required.")
                {
                    return BadRequest(result);
                }

                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{id}")]
        public ActionResult<TransactionDTO> GetById(int id)
        {
            var result = TransactionBll.GetById(id);

            if (!result.Success)
            {
                if (result.Message == "Invalid Transaction id.")
                    return BadRequest(result);

                if (result.Message == "Transaction not found.")
                    return NotFound(result);


                return StatusCode(500, result);
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<TransactionDTO> GetAll()
        {
            var result = TransactionBll.GetAll();

            if (!result.Success)
            {
                if (result.Message == "Transaction not found.")
                    return NotFound(result);
                else
                    return StatusCode(500, result);
            }
            return Ok(result.Data);
        }

        [HttpPut("status")]
        public IActionResult UpdateStatus(UpdateTransactionStatusDTO dto)
        {
            var result = TransactionBll.UpdateStatus(dto);

            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest(result);

                if (result.Message == "Transaction not found.")
                    return NotFound(result);
                if (result.Message == "Transaction already reversed.")
                    return Conflict(result);


                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }
}

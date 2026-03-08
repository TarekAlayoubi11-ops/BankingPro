using BankingPro.BLL;
using BankingPro.DAL.Context;
using BankingPro.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingPro.Controllers
{
    [Authorize]
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllActiveCustomers")]
        public ActionResult<List<CustomerDTO>> GetAllActiveCustomers()
        {
            var result = CustomerBll.GetAllActiveCustomers();

            if (!result.Success)
            {
                if (result.Message == "No customers found.")
                    return NotFound();

                return StatusCode(500, result);
            }

            return Ok(result);

        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetByIdAsync(int id, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest("Invalid Customer id.");

            ApplicationDbContext Context = new ApplicationDbContext();
            var user = Context.Customers.FirstOrDefault(u => u.CustomerId == id);

            if (user == null)
                return NotFound("Customer not found.");

            var authResult = await authorizationService.AuthorizeAsync(
                User,
                id,
                "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();

            var result = CustomerBll.GetCustomerById(id);
            if (!result.Success)
            {
                return StatusCode(500, result);
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public IActionResult Create(CreateCustomerDTO dto)
        {
            var result = CustomerBll.CreateCustomer(dto);
            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest();

                if (result.Message == "Email already exists.")
                    return Conflict();

                return StatusCode(500);
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPut]
        public IActionResult Update(UpdateCustomerDTO dto)
        {
            var result = CustomerBll.UpdateCustomer(dto);
            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest(result);

                if (result.Message == "Customer not found.")
                    return NotFound(result);

                if (result.Message == "Email already exists.")
                    return Conflict();

                return StatusCode(500, result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = CustomerBll.DeleteCustomer(id);

            if (!result.Success)
            {
                if (result.Message == "Invalid customer id.")
                    return BadRequest(result);

                if (result.Message == "Customer not found.")
                    return NotFound(result);

                return StatusCode(500, result);
            }

            return Ok(result);

        }
    }
}

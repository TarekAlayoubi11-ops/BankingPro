using BankingPro.BLL;
using BankingPro.DAL.context;
using BankingPro.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingPro.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/Users")]
    [ApiController]
    public class Users : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser(CreateUserDTO dto)
        {
            var result = UserBll.CreateUser(dto);
            if (!result.Success)
            {
                if (result.Message == "All fields are required.")
                    return BadRequest();

                if (result.Message == "Unable to create user. Please check your information.")
                    return Conflict();



                return StatusCode(500);
            }
            return Ok(result.Data);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO dto)
        {
            var result = UserBll.UpdateUser(dto);
            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest(result);

                if (result.Message == "User not found.")
                    return NotFound(result);
                if (result.Message == "Unable to updated user. Please check your information.")
                    return Conflict();
                if (result.Message == "Invalid role.")
                    return BadRequest(result);

                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = UserBll.DeleteUser(id);

            if (!result.Success)
            {
                if (result.Message == "Invalid user id.")
                    return BadRequest(result);

                if (result.Message == "User not found.")
                    return NotFound(result);

                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int id, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest("Invalid student id.");

            ApplicationDbContext Context = new ApplicationDbContext();
            var user = Context.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
                return NotFound("Student not found.");

            var authResult = await authorizationService.AuthorizeAsync(
                User,
                id,
                "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();

            return Ok(user);
        }

        [HttpGet]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {
            var result = UserBll.GetAllUsers();

            if (!result.Success)
            {
                if (result.Message == "No users found.")
                    return NotFound();

                return StatusCode(500, result);
            }

            return Ok(result);

        }
    }
}

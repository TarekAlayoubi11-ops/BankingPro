using BankingPro.BLL;
using BankingPro.DAL.Context;
using BankingPro.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingPro.Controllers
{
    [Authorize]
    [Route("api/Cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public IActionResult Create(CreateCardDTO dto)
        {
            var result = CardBll.CreateCard(dto);

            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest();

                if (result.Message == "Account not found.")
                    return NotFound();

                if (result.Message == "Account not active.")
                    return Conflict();
                if (result.Message == "Card number already exists.")
                    return Conflict();
                if (result.Message == "Invalid expiry date.")
                    return BadRequest();
                if (result.Message == "Debit already exists.")
                    return Conflict();
                if (result.Message == "Credit already exists.")
                    return BadRequest();

                return StatusCode(500);
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPut]
        public IActionResult Update(UpdateCardDTO dto)
        {
            var result = CardBll.UpdateCard(dto);

            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest();

                if (result.Message == "Card not found.")
                    return NotFound();

                if (result.Message == "Invalid input data.")
                    return BadRequest();

                return StatusCode(500);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("{id}")]
        public IActionResult Block(int id)
        {
            var result = CardBll.BlockCard(id);

            if (!result.Success)
            {
                if (result.Message == "Invalid card id.")
                    return BadRequest(result);

                if (result.Message == "Card Already Blocked.")
                    return Conflict();

                if (result.Message == "Card not found.")
                    return NotFound();

                return StatusCode(500);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDTO>> GetByIdAsync(int id, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest("Invalid Card id.");

            ApplicationDbContext Context = new ApplicationDbContext();
            var user = Context.Cards.FirstOrDefault(u => u.CardId == id);

            if (user == null)
                return NotFound("Card not found.");

            var authResult = await authorizationService.AuthorizeAsync(
                User,
                id,
                "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();


            var result = CardBll.GetCardById(id);

            if (!result.Success)
            {
                if (result.Message == "Invalid card id.")
                    return BadRequest();

                return StatusCode(500);
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<CardDTO>> GetAllActive()
        {
            var result = CardBll.GetAllActiveCards();

            if (!result.Success)
            {
                if (result.Message == "No cards found.")
                    return NotFound();
                else
                    return StatusCode(500);
            }
            return Ok(result.Data);
        }

    }
}

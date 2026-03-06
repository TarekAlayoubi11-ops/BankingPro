using BankingPro.BLL;
using BankingPro.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingPro.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/Branches")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateBranch(CreateBranchDTO dto)
        {
            var result = BranchBll.CreateBranch(dto);

            if (!result.Success)
            {
                if (result.Message == "All fields are required.")
                    return BadRequest();

                if (result.Message == "Branch already exists in this city.")
                    return Conflict();

                return StatusCode(500);
            }

            return Ok(result.Data);
        }


        [HttpPut]
        public IActionResult UpdateBranch([FromBody] UpdateBranchDTO dto)
        {
            var result = BranchBll.UpdateBranch(dto);

            if (!result.Success)
            {
                if (result.Message == "Invalid input data.")
                    return BadRequest(result);

                if (result.Message == "Branch not found.")
                    return NotFound(result);

                if (result.Message == "Branch already exists in this city.")
                    return Conflict(result);

                return StatusCode(500, result);
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBranch(int id)
        {
            var result = BranchBll.DeleteBranch(id);

            if (!result.Success)
            {
                if (result.Message == "Invalid branch id.")
                    return BadRequest(result);

                if (result.Message == "Branch not found.")
                    return NotFound(result);

                return StatusCode(500, result);
            }

            return Ok(result);
        }


        [HttpGet("{id}")]
        public ActionResult<BranchDTO> GetBranchById(int id)
        {
            var result = BranchBll.GetBranchById(id);

            if (!result.Success)
            {
                if (result.Message == "Branch not found.")
                    return NotFound();

                return StatusCode(500, result);
            }

            return Ok(result.Data);
        }


        [HttpGet("ActiveBranches")]
        public ActionResult<List<BranchDTO>> GetAllActiveBranches()
        {
            var result = BranchBll.GetAllActiveBranches();

            if (!result.Success)
            {
                if (result.Message == "No branches found.")
                    return NotFound();

                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }
}

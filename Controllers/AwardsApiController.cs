using BallonDor.DTO;
using BallonDor.Services;
using Microsoft.AspNetCore.Mvc;

namespace BallonDor.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsApiController : ControllerBase
    {
        private readonly IAwardService _awardService;

        public AwardsApiController(IAwardService awardService)
        {
            _awardService = awardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AwardDTO>>> GetAwards()
        {
            return Ok(await _awardService.GetAllAwardsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AwardDTO>> GetAward(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();

            return Ok(award);
        }

        [HttpPost]
        public async Task<ActionResult<AwardDTO>> CreateAward(AwardDTO awardDto)
        {
            var createdAward = await _awardService.CreateAwardAsync(awardDto);
            return CreatedAtAction(nameof(GetAward), new { id = createdAward.AwardId }, createdAward);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAward(int id, AwardDTO awardDto)
        {
            if (id != awardDto.AwardId) return BadRequest();

            await _awardService.UpdateAwardAsync(awardDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            await _awardService.DeleteAwardAsync(id);
            return NoContent();
        }
    }
}
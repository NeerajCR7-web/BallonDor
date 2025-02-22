using BallonDor.DTO;
using BallonDor.Services;
using Microsoft.AspNetCore.Mvc;

namespace BallonDor.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotersApiController : ControllerBase
    {
        private readonly IVoterService _voterService;

        public VotersApiController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoterDTO>>> GetVoters()
        {
            return Ok(await _voterService.GetAllVotersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoterDTO>> GetVoter(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();

            return Ok(voter);
        }

        [HttpPost]
        public async Task<ActionResult<VoterDTO>> CreateVoter(VoterDTO voterDto)
        {
            var createdVoter = await _voterService.CreateVoterAsync(voterDto);
            return CreatedAtAction(nameof(GetVoter), new { id = createdVoter.VoterId }, createdVoter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoter(int id, VoterDTO voterDto)
        {
            if (id != voterDto.VoterId) return BadRequest();

            await _voterService.UpdateVoterAsync(voterDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoter(int id)
        {
            await _voterService.DeleteVoterAsync(id);
            return NoContent();
        }
    }
}
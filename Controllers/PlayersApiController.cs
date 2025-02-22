using BallonDor.DTO;
using BallonDor.Services;
using Microsoft.AspNetCore.Mvc;

namespace BallonDor.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersApiController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersApiController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
        {
            return Ok(await _playerService.GetAllPlayersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null) return NotFound();

            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<PlayerDTO>> CreatePlayer(PlayerDTO playerDto)
        {
            var createdPlayer = await _playerService.CreatePlayerAsync(playerDto);
            return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.PlayerId }, createdPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, PlayerDTO playerDto)
        {
            if (id != playerDto.PlayerId) return BadRequest();

            await _playerService.UpdatePlayerAsync(playerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            await _playerService.DeletePlayerAsync(id);
            return NoContent();
        }
    }
}
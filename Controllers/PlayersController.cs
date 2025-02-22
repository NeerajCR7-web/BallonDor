using BallonDor.DTO;
using BallonDor.Services;
using Microsoft.AspNetCore.Mvc;

namespace BallonDor.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return View(players);
        }

        public async Task<IActionResult> Details(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null) return NotFound();

            return View(player);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerDTO playerDto)
        {
            if (ModelState.IsValid)
            {
                await _playerService.CreatePlayerAsync(playerDto);
                return RedirectToAction(nameof(Index));
            }
            return View(playerDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null) return NotFound();

            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlayerDTO playerDto)
        {
            if (id != playerDto.PlayerId) return NotFound();

            if (ModelState.IsValid)
            {
                await _playerService.UpdatePlayerAsync(playerDto);
                return RedirectToAction(nameof(Index));
            }
            return View(playerDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null) return NotFound();

            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _playerService.DeletePlayerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
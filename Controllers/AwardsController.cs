using BallonDor.Models;
using BallonDor.DTO;
using BallonDor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BallonDor.Controllers
{
    public class AwardsController : Controller
    {
        private readonly IAwardService _awardService;
        private readonly IPlayerService _playerService;
        private readonly IVoterService _voterService;

        public AwardsController(IAwardService awardService, IPlayerService playerService, IVoterService voterService)
        {
            _awardService = awardService;
            _playerService = playerService;
            _voterService = voterService;
        }

        // GET: Awards
        public async Task<IActionResult> Index()
        {
            var awards = await _awardService.GetAllAwardsAsync();
            return View(awards);
        }

        // GET: Awards/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }

        // GET: Awards/Create
        public async Task<IActionResult> Create()
        {
            // Fetch players and voters for dropdowns
            var players = await _playerService.GetAllPlayersAsync();
            var voters = await _voterService.GetAllVotersAsync();

            // Populate ViewBag with players and voters
            ViewBag.Players = new SelectList(players, "PlayerId", "Name");
            ViewBag.Voters = new SelectList(voters, "VoterId", "Name");

            return View();
        }

        // POST: Awards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AwardDTO awardDto)
        {
            if (ModelState.IsValid)
            {
                await _awardService.CreateAwardAsync(awardDto);
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, repopulate the dropdowns
            var players = await _playerService.GetAllPlayersAsync();
            var voters = await _voterService.GetAllVotersAsync();

            ViewBag.Players = new SelectList(players, "PlayerId", "Name", awardDto.PlayerId);
            ViewBag.Voters = new SelectList(voters, "VoterId", "Name", awardDto.VoterId);

            return View(awardDto);
        }

        // GET: Awards/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }

            // Fetch players and voters for dropdowns
            var players = await _playerService.GetAllPlayersAsync();
            var voters = await _voterService.GetAllVotersAsync();

            // Populate ViewBag with players and voters
            ViewBag.Players = new SelectList(players, "PlayerId", "Name", award.PlayerId);
            ViewBag.Voters = new SelectList(voters, "VoterId", "Name", award.VoterId);

            return View(award);
        }

        // POST: Awards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AwardDTO awardDto)
        {
            if (id != awardDto.AwardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _awardService.UpdateAwardAsync(awardDto);
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, repopulate the dropdowns
            var players = await _playerService.GetAllPlayersAsync();
            var voters = await _voterService.GetAllVotersAsync();

            ViewBag.Players = new SelectList(players, "PlayerId", "Name", awardDto.PlayerId);
            ViewBag.Voters = new SelectList(voters, "VoterId", "Name", awardDto.VoterId);

            return View(awardDto);
        }

        // GET: Awards/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null)
            {
                return NotFound();
            }
            return View(award);
        }

        // POST: Awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _awardService.DeleteAwardAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
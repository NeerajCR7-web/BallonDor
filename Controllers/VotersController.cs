using BallonDor.DTO;
using BallonDor.Services;
using Microsoft.AspNetCore.Mvc;

namespace BallonDor.Controllers
{
    public class VotersController : Controller
    {
        private readonly IVoterService _voterService;

        public VotersController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        public async Task<IActionResult> Index()
        {
            var voters = await _voterService.GetAllVotersAsync();
            return View(voters);
        }

        public async Task<IActionResult> Details(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();

            return View(voter);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoterDTO voterDto)
        {
            if (ModelState.IsValid)
            {
                await _voterService.CreateVoterAsync(voterDto);
                return RedirectToAction(nameof(Index));
            }
            return View(voterDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();

            return View(voter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VoterDTO voterDto)
        {
            if (id != voterDto.VoterId) return NotFound();

            if (ModelState.IsValid)
            {
                await _voterService.UpdateVoterAsync(voterDto);
                return RedirectToAction(nameof(Index));
            }
            return View(voterDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();

            return View(voter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _voterService.DeleteVoterAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
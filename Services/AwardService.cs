using BallonDor.Data;
using BallonDor.DTO;
using BallonDor.Models;
using Microsoft.EntityFrameworkCore;

namespace BallonDor.Services
{
    public class AwardService : IAwardService
    {
        private readonly ApplicationDbContext _context;

        public AwardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AwardDTO>> GetAllAwardsAsync()
        {
            return await _context.Awards
                .Include(a => a.Player)
                .Include(a => a.Voter)
                .Select(a => new AwardDTO
                {
                    AwardId = a.AwardId,
                    Year = a.Year,
                    Description = a.Description,
                    PlayerId = a.PlayerId,
                    VoterId = a.VoterId
                }).ToListAsync();
        }

        public async Task<AwardDTO> GetAwardByIdAsync(int id)
        {
            var award = await _context.Awards
                .Include(a => a.Player)
                .Include(a => a.Voter)
                .FirstOrDefaultAsync(a => a.AwardId == id);

            if (award == null) return null;

            return new AwardDTO
            {
                AwardId = award.AwardId,
                Year = award.Year,
                Description = award.Description,
                PlayerId = award.PlayerId,
                VoterId = award.VoterId
            };
        }

        public async Task<AwardDTO> CreateAwardAsync(AwardDTO awardDto)
        {
            var award = new Award
            {
                Year = awardDto.Year,
                Description = awardDto.Description,
                PlayerId = awardDto.PlayerId,
                VoterId = awardDto.VoterId
            };

            _context.Awards.Add(award);
            await _context.SaveChangesAsync();

            awardDto.AwardId = award.AwardId;
            return awardDto;
        }

        public async Task UpdateAwardAsync(AwardDTO awardDto)
        {
            var award = await _context.Awards.FindAsync(awardDto.AwardId);
            if (award == null) throw new Exception("Award not found");

            award.Year = awardDto.Year;
            award.Description = awardDto.Description;
            award.PlayerId = awardDto.PlayerId;
            award.VoterId = awardDto.VoterId;

            _context.Awards.Update(award);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAwardAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award == null) throw new Exception("Award not found");

            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();
        }
    }
}
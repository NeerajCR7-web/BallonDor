using BallonDor.Data;
using BallonDor.DTO;
using BallonDor.Models;
using Microsoft.EntityFrameworkCore;

namespace BallonDor.Services
{
    public class VoterService : IVoterService
    {
        private readonly ApplicationDbContext _context;

        public VoterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VoterDTO>> GetAllVotersAsync()
        {
            return await _context.Voters
                .Include(v => v.Awards)
                .Select(v => new VoterDTO
                {
                    VoterId = v.VoterId,
                    Name = v.Name,
                    Country = v.Country,
                    Role = v.Role,
                    Awards = v.Awards.Select(a => new AwardDTO
                    {
                        AwardId = a.AwardId,
                        Year = a.Year,
                        Description = a.Description,
                        PlayerId = a.PlayerId,
                        VoterId = a.VoterId
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<VoterDTO?> GetVoterByIdAsync(int id)
        {
            var voter = await _context.Voters
                .Include(v => v.Awards)
                .FirstOrDefaultAsync(v => v.VoterId == id);

            if (voter == null) return null;

            return new VoterDTO
            {
                VoterId = voter.VoterId,
                Name = voter.Name,
                Country = voter.Country,
                Role = voter.Role,
                Awards = voter.Awards.Select(a => new AwardDTO
                {
                    AwardId = a.AwardId,
                    Year = a.Year,
                    Description = a.Description,
                    PlayerId = a.PlayerId,
                    VoterId = a.VoterId
                }).ToList()
            };
        }

        public async Task<VoterDTO> CreateVoterAsync(VoterDTO voterDto)
        {
            var voter = new Voter
            {
                Name = voterDto.Name,
                Country = voterDto.Country,
                Role = voterDto.Role
            };

            _context.Voters.Add(voter);
            await _context.SaveChangesAsync();

            voterDto.VoterId = voter.VoterId;
            return voterDto;
        }

        public async Task UpdateVoterAsync(VoterDTO voterDto)
        {
            var voter = await _context.Voters.FindAsync(voterDto.VoterId);
            if (voter == null) throw new Exception("Voter not found");

            voter.Name = voterDto.Name;
            voter.Country = voterDto.Country;
            voter.Role = voterDto.Role;

            _context.Voters.Update(voter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVoterAsync(int id)
        {
            var voter = await _context.Voters.FindAsync(id);
            if (voter == null) throw new Exception("Voter not found");

            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync();
        }
        public interface IVoterService
        {
            Task<VoterDTO?> GetVoterByIdAsync(int id);
        }
    }
}
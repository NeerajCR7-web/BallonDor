using BallonDor.Data;
using BallonDor.DTO;
using BallonDor.Models;
using Microsoft.EntityFrameworkCore;

namespace BallonDor.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlayerDTO>> GetAllPlayersAsync()
        {
            return await _context.Players
                .Include(p => p.Awards)
                .Select(p => new PlayerDTO
                {
                    PlayerId = p.PlayerId,
                    Name = p.Name,
                    Position = p.Position,
                    Nationality = p.Nationality,
                    DateOfBirth = p.DateOfBirth,
                    Awards = p.Awards.Select(a => new AwardDTO
                    {
                        AwardId = a.AwardId,
                        Year = a.Year,
                        Description = a.Description,
                        PlayerId = a.PlayerId,
                        VoterId = a.VoterId
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<PlayerDTO> GetPlayerByIdAsync(int id)
        {
            var player = await _context.Players
                .Include(p => p.Awards)
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            if (player == null) return null;

            return new PlayerDTO
            {
                PlayerId = player.PlayerId,
                Name = player.Name,
                Position = player.Position,
                Nationality = player.Nationality,
                DateOfBirth = player.DateOfBirth,
                Awards = player.Awards.Select(a => new AwardDTO
                {
                    AwardId = a.AwardId,
                    Year = a.Year,
                    Description = a.Description,
                    PlayerId = a.PlayerId,
                    VoterId = a.VoterId
                }).ToList()
            };
        }

        public async Task<PlayerDTO> CreatePlayerAsync(PlayerDTO playerDto)
        {
            var player = new Player
            {
                Name = playerDto.Name,
                Position = playerDto.Position,
                Nationality = playerDto.Nationality,
                DateOfBirth = playerDto.DateOfBirth
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            playerDto.PlayerId = player.PlayerId;
            return playerDto;
        }

        public async Task UpdatePlayerAsync(PlayerDTO playerDto)
        {
            var player = await _context.Players.FindAsync(playerDto.PlayerId);
            if (player == null) throw new Exception("Player not found");

            player.Name = playerDto.Name;
            player.Position = playerDto.Position;
            player.Nationality = playerDto.Nationality;
            player.DateOfBirth = playerDto.DateOfBirth;

            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) throw new Exception("Player not found");

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
    }
}
using BallonDor.DTO;

namespace BallonDor.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDTO>> GetAllPlayersAsync();
        Task<PlayerDTO> GetPlayerByIdAsync(int id);
        Task<PlayerDTO> CreatePlayerAsync(PlayerDTO playerDto);
        Task UpdatePlayerAsync(PlayerDTO playerDto);
        Task DeletePlayerAsync(int id);
    }
}
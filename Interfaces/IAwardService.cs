using BallonDor.DTO;

namespace BallonDor.Services
{
    public interface IAwardService
    {
        Task<IEnumerable<AwardDTO>> GetAllAwardsAsync();
        Task<AwardDTO> GetAwardByIdAsync(int id);
        Task<AwardDTO> CreateAwardAsync(AwardDTO awardDto);
        Task UpdateAwardAsync(AwardDTO awardDto);
        Task DeleteAwardAsync(int id);
    }
}
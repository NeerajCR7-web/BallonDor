using BallonDor.DTO;

namespace BallonDor.Services
{
    public interface IVoterService
    {
        Task<IEnumerable<VoterDTO>> GetAllVotersAsync();
        Task<VoterDTO> GetVoterByIdAsync(int id);
        Task<VoterDTO> CreateVoterAsync(VoterDTO voterDto);
        Task UpdateVoterAsync(VoterDTO voterDto);
        Task DeleteVoterAsync(int id);
    }
}
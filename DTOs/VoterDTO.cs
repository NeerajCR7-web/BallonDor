namespace BallonDor.DTO
{
    public class VoterDTO
    {
        public int VoterId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public List<AwardDTO> Awards { get; set; } = new List<AwardDTO>();
    }
}
namespace BallonDor.DTO
{
    public class AwardDTO
    {
        public int AwardId { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int PlayerId { get; set; }
        public int VoterId { get; set; }
    }
}
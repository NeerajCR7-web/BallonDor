namespace BallonDor.DTO
{
    public class PlayerDTO
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<AwardDTO> Awards { get; set; } = new List<AwardDTO>();
    }
}
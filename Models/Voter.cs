namespace BallonDor.Models
{
    public class Voter
    {
        public int VoterId { get; set; } // Primary key
        public string Name { get; set; } // Voter's name
        public string Country { get; set; } // Voter's country
        public string Role { get; set; } // Voter's role (e.g., Journalist, Coach)

        // Navigation property for Awards voted by this voter
        public ICollection<Award> Awards { get; set; } = new List<Award>();
    }
}

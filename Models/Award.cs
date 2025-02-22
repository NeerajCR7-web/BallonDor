namespace BallonDor.Models
{
    public class Award
    {
        public int AwardId { get; set; } // Primary key
        public int Year { get; set; } // Year the award was given
        public string Description { get; set; } // Additional details about the award

        // Foreign key and navigation property for the Player who won the award
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        // Foreign key and navigation property for the Voter who voted for the award
        public int VoterId { get; set; }
        public Voter Voter { get; set; }
    }
}

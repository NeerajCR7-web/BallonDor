namespace BallonDor.Models
{
    public class Player
    {
        public int PlayerId { get; set; } // Primary key
        public string Name { get; set; } // Player's name
        public string Position { get; set; } // Player's position (e.g., Forward, Midfielder)
        public string Nationality { get; set; } // Player's nationality
        public DateTime DateOfBirth { get; set; } // Player's date of birth

        // Navigation property for Awards won by the player
        public ICollection<Award> Awards { get; set; } = new List<Award>();
    }
}

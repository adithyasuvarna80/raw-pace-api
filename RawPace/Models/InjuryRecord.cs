namespace RawPace.Models
{
    public class InjuryRecord
    {
        public int Id { get; set; }

        // This links the record to a specific bowler
        public int BowlerId { get; set; }

        // What their status changed to (Active, Injured, Recovering)
        public string Status { get; set; } = string.Empty;

        // Exactly when it happened
        public DateTime DateRecorded { get; set; }

        // Optional notes from the scout (e.g., "Hamstring strain during training")
        public string Notes { get; set; } = string.Empty;
    }
}
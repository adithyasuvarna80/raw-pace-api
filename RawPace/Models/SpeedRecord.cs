namespace RawPace.Models
{
    public class SpeedRecord
    {
        public int Id { get; set; }
        public int BowlerId { get; set; }
        public double TopSpeed { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
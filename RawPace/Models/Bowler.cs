namespace RawPace.Models
{
    public class Bowler
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double TopSpeed { get; set; }
        public double AvgSpeed { get; set; }
        public string Status { get; set; } = string.Empty;
        public double OversBowled { get; set; }
        public string Specialty { get; set; } = string.Empty;
    }
}

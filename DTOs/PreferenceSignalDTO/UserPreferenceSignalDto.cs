namespace CairoGo.DTOs.PreferenceSignalDTO
{
    public class UserPreferenceSignalDto
    {
        public Guid SignalId { get; set; }
        public Guid UserId { get; set; }
        public string SignalType { get; set; } = default!;
        public string Key { get; set; } = default!;
        public float Weight { get; set; }
        public int ObservationCount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

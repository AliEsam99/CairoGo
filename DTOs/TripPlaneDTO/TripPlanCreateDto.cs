namespace CairoGo.DTOs.TripPlaneDTO
{
    public class TripPlanCreateDto
    {
        public Guid UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; } = DateTime.UtcNow;

        public int TripDays { get; set; }

        public decimal? EstimatedCostPerPersonPerDay { get; set; }

        public decimal? AlignmentScore { get; set; }

        public string? ScoreBreakdownJson { get; set; }

        // Optional: لتخطيط الرحلات البديلة
        public Guid? ParentPlanId { get; set; }

        public int VariationNumber { get; set; } = 1;

        // Optional: PreferenceProfileId
        public Guid? PreferenceProfileId { get; set; }
    }
}

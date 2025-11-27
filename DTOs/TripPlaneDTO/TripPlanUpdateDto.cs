namespace CairoGo.DTOs.TripPlaneDTO
{
    public class TripPlanUpdateDto
    {
        public string Title { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public int TripDays { get; set; }

        public decimal? EstimatedCostPerPersonPerDay { get; set; }

        public decimal? AlignmentScore { get; set; }

        public string? ScoreBreakdownJson { get; set; }

        // Optional: تعديل الرحلات البديلة
        public Guid? ParentPlanId { get; set; }

        public int VariationNumber { get; set; } = 1;

        public Guid? PreferenceProfileId { get; set; }
    }
}

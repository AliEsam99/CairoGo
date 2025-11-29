namespace CairoGo.DTOs.ExperimentAssignmentDTO
{
    public class ExperimentAssignmentUpdateDto
    {
        public DateTime? CompletedAt { get; set; }
        public int? TripPlansGenerated { get; set; }
        public int? PlacesClicked { get; set; }
        public bool? ConversionSuccess { get; set; }
    }
}

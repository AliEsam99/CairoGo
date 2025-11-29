namespace CairoGo.DTOs.ExperimentAssignmentDTO
{
    public class ExperimentAssignmentResponseDto
    {
        public Guid AssignmentId { get; set; }
        public Guid UserId { get; set; }
        public string ExperimentName { get; set; }
        public string VariantName { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? TripPlansGenerated { get; set; }
        public int? PlacesClicked { get; set; }
        public bool? ConversionSuccess { get; set; }
    }
}

namespace CairoGo.DTOs.SearchSessionDTO
{
    public class SearchSessionResponseDto
    {

        public Guid SearchSessionId { get; set; }
        public Guid? UserId { get; set; }
        public string? SearchQuery { get; set; }
        public string? SearchParamsJson { get; set; }
        public int ResultCount { get; set; }
        public List<Guid>? ClickedPlaceIds { get; set; }
        public int? SelectedPosition { get; set; }
        public TimeSpan? TimeToClick { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

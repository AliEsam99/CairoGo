namespace CairoGo.DTOs.PlaceOperatingHoursDTO
{
    public class PlaceOperatingHoursResponseDto
    {
        public Guid PlaceOperatingHoursId { get; set; }
        public Guid PlaceId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public bool IsClosed { get; set; }
    }
}

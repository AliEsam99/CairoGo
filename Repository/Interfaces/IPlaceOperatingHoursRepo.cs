using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface IPlaceOperatingHoursRepo:IBaseRepo<PlaceOperatingHours>
    {
        Task<List<PlaceOperatingHours>> GetByPlaceIdAsync(Guid placeId);
        Task<PlaceOperatingHours> GetByPlaceAndDayAsync(Guid placeId,DayOfWeek day);
    }
}

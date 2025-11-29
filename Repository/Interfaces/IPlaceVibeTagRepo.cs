using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface IPlaceVibeTagRepo : IBaseRepo<PlaceVibeTag>
    {
        Task<List<PlaceVibeTag>> GetByPlaceIdAsync(Guid placeId);

    }
}

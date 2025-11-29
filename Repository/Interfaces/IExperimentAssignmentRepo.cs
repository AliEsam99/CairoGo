using CairoGo.Models.Entity;

namespace CairoGo.Repository.Interfaces
{
    public interface IExperimentAssignmentRepo:IBaseRepo<ExperimentAssignment>
    {
        Task<List<ExperimentAssignment>> GetAssignmentsByUserIdAsync(Guid userId);
        Task<ExperimentAssignment?> GetUserExperimentAsync(Guid userId,string experimentName);
    }
}

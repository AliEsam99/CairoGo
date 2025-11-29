using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Repository.Implementations
{
    public class ExperimentAssignmentRepo : Repository<ExperimentAssignment>, IExperimentAssignmentRepo
    {
        public ExperimentAssignmentRepo(CairoGoDbContext Db) : base(Db)
        {

        }
       
        public async Task<List<ExperimentAssignment>> GetAssignmentsByUserIdAsync(Guid userId)
        {
            return await _DbSet.Where(x=>x.UserId==userId).ToListAsync();
        }

        public async Task<ExperimentAssignment?> GetUserExperimentAsync(Guid userId, string experimentName)
        {
            return await _DbSet.FirstOrDefaultAsync(x => x.UserId == userId
            && x.ExperimentName == experimentName);
        }
    }
}

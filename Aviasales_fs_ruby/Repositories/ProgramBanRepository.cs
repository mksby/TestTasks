using System.Linq;
using TestTasks.Data;
using TestTasks.DTO;
using TestTasks.Interfaces;

namespace TestTasks.Repositories
{
    public class ProgramBanRepository: BaseRepository<ProgramBan, int>, IProgramBanRepository<ProgramBan, int>
    {
        public ProgramBanRepository(AviasalesContext context) : base(context)
        {
            
        }

        public ProgramBan GetByUserProgramIds(int userId, int programId)
        {
            return Set.FirstOrDefault(programBan => programBan.UserId == userId && programBan.ProgramId == programId);
        }

        public IQueryable<ProgramBan> GetUserPrograms(int userId)
        {
            return Set.Where(programBan => programBan.UserId == userId);
        }
    }
}
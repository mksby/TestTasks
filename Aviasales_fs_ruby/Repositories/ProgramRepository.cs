using TestTasks.Data;
using TestTasks.DTO;
using TestTasks.Interfaces;

namespace TestTasks.Repositories
{
    public class ProgramRepository: BaseRepository<TestTasks.DTO.Program, int>, IProgramRepository<TestTasks.DTO.Program, int>
    {
        public ProgramRepository(AviasalesContext context) : base(context)
        {
            
        }
    }
}
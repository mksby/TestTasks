using System.Linq;
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

        public IQueryable<TestTasks.DTO.Program> GetByIds(int[] programIds)
        {
            return Set.Where(program => programIds.Any(pid => pid == program.Id));
        }

        public IQueryable<DTO.Program> GetByTerm(string term)
        {
            return Set.Where(program => program.Name.ToLower().Contains(term.ToLower()));
        }
    }
}
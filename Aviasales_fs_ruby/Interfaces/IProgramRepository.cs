using System.Linq;

namespace TestTasks.Interfaces
{
    public interface IProgramRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> 
        where TEntity : class
        where TKey: struct
    {
        IQueryable<TestTasks.DTO.Program> GetByIds(int[] programIds);
        
        IQueryable<TestTasks.DTO.Program> GetByTerm(string term);
    }
}
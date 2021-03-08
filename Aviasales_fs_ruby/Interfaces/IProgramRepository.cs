using System.Linq;

namespace TestTasks.Interfaces
{
    public interface IProgramRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> 
        where TEntity : class
        where TKey: struct
    {
        IQueryable<TEntity> GetByIds(int[] programIds);
        
        IQueryable<TEntity> GetByTerm(string term);
    }
}
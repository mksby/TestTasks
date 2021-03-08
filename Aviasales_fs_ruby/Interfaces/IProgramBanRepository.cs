using System.Linq;

namespace TestTasks.Interfaces
{
    public interface IProgramBanRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct
    {
        TEntity GetByUserProgramIds(int userId, int programId);
        
        IQueryable<TEntity> GetUserPrograms(int userId);
    }
}
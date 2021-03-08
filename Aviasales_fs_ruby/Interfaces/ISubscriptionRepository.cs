using System.Collections.Generic;
using System.Linq;
using TestTasks.DTO;

namespace TestTasks.Interfaces
{
    public interface ISubscriptionRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> 
        where TEntity : class
        where TKey: struct
    {
        IQueryable<TEntity> GetByUserId(int userId);
        
        TEntity GetByUserProgramIds(int userId, int programId);
        
        IQueryable<TEntity> GetByProgramId(int programId);
    }
}
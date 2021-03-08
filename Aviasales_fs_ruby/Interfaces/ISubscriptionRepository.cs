using System.Collections.Generic;
using System.Linq;
using TestTasks.DTO;

namespace TestTasks.Interfaces
{
    public interface ISubscriptionRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> 
        where TEntity : class
        where TKey: struct
    {
        IQueryable<Subscription> GetByUserId(int userId);
        
        Subscription GetByUserProgramIds(int userId, int programId);
        
        IQueryable<Subscription> GetByProgramId(int programId);
    }
}
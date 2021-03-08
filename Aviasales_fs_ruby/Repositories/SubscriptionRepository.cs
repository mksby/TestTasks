using System.Collections.Generic;
using System.Linq;
using TestTasks.Data;
using TestTasks.DTO;
using TestTasks.Interfaces;

namespace TestTasks.Repositories
{
    public class SubscriptionRepository: BaseRepository<Subscription, int>, ISubscriptionRepository<Subscription, int>
    {
        public SubscriptionRepository(AviasalesContext context) : base(context)
        {
            
        }
        
        public IQueryable<Subscription> GetByUserId(int userId)
        {
            return Set.Where(subscription => subscription.UserId == userId);
        }

        public Subscription GetByUserProgramIds(int userId, int programId)
        {
            return Set.FirstOrDefault(subscription => subscription.UserId == userId && subscription.ProgramId == programId);
        }

        public IQueryable<Subscription> GetByProgramId(int programId)
        {
            return Set.Where(subscription => subscription.ProgramId == programId);
        }
    }
}
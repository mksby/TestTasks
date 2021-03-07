using TestTasks.Data;
using TestTasks.DTO;
using TestTasks.Interfaces;

namespace TestTasks.Repositories
{
    public class UserRepository: BaseRepository<User, int>, IUserRepository<User, int>
    {
        public UserRepository(AviasalesContext context) : base(context)
        {
            
        }
    }
}
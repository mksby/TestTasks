using System.Linq;
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
        
        public User GetByEmail(string userEmail)
        {
            return Set.FirstOrDefault(user => user.Email == userEmail);
        }
    }
}
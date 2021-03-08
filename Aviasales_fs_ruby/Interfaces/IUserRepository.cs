using TestTasks.DTO;

namespace TestTasks.Interfaces
{
    public interface IUserRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> 
        where TEntity : class
        where TKey: struct
    {
        TEntity GetByEmail(string userEmail);
    }
}
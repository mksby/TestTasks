namespace TestTasks.Interfaces
{
    public interface IUserRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> 
        where TEntity : class
        where TKey: struct
    {
    }
}
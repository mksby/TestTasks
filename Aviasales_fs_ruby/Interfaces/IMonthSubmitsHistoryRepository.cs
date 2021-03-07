namespace TestTasks.Interfaces
{
    public interface IProgramRepository<TEntity, TKey>: IBaseRepository<TEntity, TKey> 
        where TEntity : class
        where TKey: struct
    {
    }
}
using System.Collections.Generic;

namespace TestTasks.Interfaces
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class where TKey : struct
    {
        IEnumerable<TEntity> GetAll();
        void SaveChanges();
        TEntity Insert(TEntity item, bool commit = true);
        void Attach(TEntity item);
        void Detach(TEntity item);
        void Delete(TKey id, bool commit = true);
        void Delete(TEntity item, bool commit = true);
        void Update(TEntity item, bool commit = true);
        TEntity GetById(TKey id);
        TEntity Upsert(TEntity item, TKey id, bool commit = true);
        void Dispose();
    }
}
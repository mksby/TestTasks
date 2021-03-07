using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestTasks.Interfaces;

namespace TestTasks.Repositories
{
    public class BaseRepository<TEntity, TKey> : IDisposable, IBaseRepository<TEntity, TKey> where TEntity : class
            where TKey: struct
    {
        protected readonly DbContext DbContext;

        internal BaseRepository(DbContext context)
        {
            DbContext = context;
        }

        protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Set.AsEnumerable();
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public virtual TEntity Insert(TEntity item, bool commit = true)
        {
            Set.Add(item);
            if (commit) SaveChanges();
            return item;
        }

        public void Attach(TEntity item)
        {
            Set.Attach(item);
        }

        public void Detach(TEntity item)
        {
            DbContext.Entry(item).State = EntityState.Detached;
        }

        public virtual void Delete(TKey id, bool commit = true)
        {
            var item = Set.Find(id);
            if (item != null)
                Delete(item, commit);
        }

        public virtual void Delete(TEntity item, bool commit = true)
        {
            Set.Remove(item);
            if (commit) SaveChanges();
        }

        public virtual void Update(TEntity item, bool commit = true)
        {
            DbContext.Entry(item).State = EntityState.Modified;
            if (commit) SaveChanges();
        }

        public virtual TEntity GetById(TKey id)
        {
            return Set.Find(id);
        }

        public virtual TEntity Upsert(TEntity item, TKey id, bool commit = true)
        {
            if (id.Equals(default(TKey)))
            {
                if (DbContext.Entry(item).State != EntityState.Detached)
                {
                    Detach(item);
                }

                Insert(item, commit);
                return item;
            }

            try
            {
                Update(item, false);
            }
            catch (InvalidOperationException)
            {
                var existing = GetById(id);
                DbContext.Entry(existing).CurrentValues.SetValues(item);
            }

            if (commit) SaveChanges();
            return item;
        }

        public virtual void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
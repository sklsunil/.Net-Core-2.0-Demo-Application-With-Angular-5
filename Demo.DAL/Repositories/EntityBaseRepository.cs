using Demo.DAL.Abstract;
using Demo.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public class EntityBaseRepository<T, Key> : IEntityBaseRepository<T, Key> where T : class, IEntityBase, new()
    {
        private DemoDbContext db;    //my database context
        private DbSet<T> entities;  //specific set

        protected EntityBaseRepository(DemoDbContext db)
        {
            entities = db.Set<T>();
            this.db = db;
        }

        public async Task<T> AddAsync(T entity)
        {
            await entities.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(Key id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<bool> DeleteAsync(Key id)
        {
            T objectToDelete = await entities.FindAsync(id);
            if (objectToDelete == null)
            {
                return false;
            }
            entities.Remove(objectToDelete);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(T entity)
        {
            entities.Remove(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(List<T> items)
        {
            foreach (T entity in items)
            {
                entities.Remove(entity);
            }
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entities.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

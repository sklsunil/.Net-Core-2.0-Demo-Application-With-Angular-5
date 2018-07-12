using Demo.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Abstract
{
    public interface IEntityBaseRepository<T, key> : IDisposable where T : class, IEntityBase, new()
    {
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(key id);
        Task<List<T>> GetAllAsync();
        Task<bool> DeleteAsync(key id);
        Task DeleteAsync(T entity);
        Task DeleteAsync(List<T> items);
        Task UpdateAsync(T entity);        
    }
}

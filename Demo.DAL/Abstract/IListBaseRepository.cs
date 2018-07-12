using Demo.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Abstract
{
    public interface IListBaseRepository<T> where T : class, IEntityBase, new()
    {
        T GetById(int id);
        List<T> GetAll();
        void Update(T entity);
    }
}

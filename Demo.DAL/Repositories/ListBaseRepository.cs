using Demo.DAL.Abstract;
using Demo.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Demo.DAL.Repositories
{
    public class ListBaseRepository<T> : IListBaseRepository<T> where T : class, IEntityBase, new()
    {
        private static List<T> entities;  //specific set

        protected ListBaseRepository(ref List<T> list)
        {
            entities = list;
        }

        public List<T> GetAll()
        {
            return entities;
        }

        public T GetById(int id)
        {
            return entities.Find(x => x.Id == id);
        }

        public void Update(T entity)
        {
            int index = entities.FindIndex(x => x.Id == entity.Id);
            if(index <= 0)
            {
                throw new Exception("Element does not found.");
            }
            entities[index] = entity;
        }
    }
}

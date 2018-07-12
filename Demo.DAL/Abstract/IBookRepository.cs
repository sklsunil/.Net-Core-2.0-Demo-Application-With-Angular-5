using Demo.DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DAL.Abstract
{
    public interface IBookRepository : IEntityBaseRepository<Book,int>
    {

    }
}

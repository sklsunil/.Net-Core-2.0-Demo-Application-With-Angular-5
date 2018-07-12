using Demo.DAL.Abstract;
using Demo.DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DAL.Repositories
{
    public class BookRepository : EntityBaseRepository<Book, int>, IBookRepository
    {
        private DemoDbContext db;
        public BookRepository(DemoDbContext db) : base(db)
        {
        }
    }
}

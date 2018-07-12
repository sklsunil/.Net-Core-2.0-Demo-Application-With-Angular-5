using Demo.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Demo.DomainModels.Entities;

namespace Demo.XUnitTest
{
    /// <summary>
    /// Database Fixture Setup
    /// </summary>
    public class DatabaseFixture : IDisposable
    {
        /// <summary>
        /// DataContext object declaration
        /// </summary>
        public readonly DemoDbContext _context;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseFixture()
        {

            //Set builder
            var builder = new DbContextOptionsBuilder<DemoDbContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            var options = builder.Options;




            _context = new DemoDbContext(options);
            _context.Database.EnsureCreated();

            //Seed default test data on Initialization
            Seed();
        }

        #endregion

        private void Seed()
        {
            //SITE Initilization
            if (!_context.Book.Any())
            {
                _context.AddRange(bookCollections);
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Test Books Data
        /// </summary>
        public static List<Demo.DomainModels.Entities.Book> bookCollections => new List<Demo.DomainModels.Entities.Book>
        {
         new Demo.DomainModels.Entities.Book
         {
             Authors    =   "Book Authors",
             DateOfPublication  =   DateTime.Now,
             Name   =   "Book Name",
             NumberOfPages  =   120
         }
        };

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }


    /// <summary>
    /// Global Database Collection object
    /// </summary>
    [CollectionDefinition("TestDb")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}

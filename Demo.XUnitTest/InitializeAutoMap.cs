using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.DAL;
using Demo.DAL.Abstract;
using Demo.DAL.Repositories;

namespace Demo.XUnitTest
{
    /// <summary>
    /// Initialize Object
    /// </summary>
    public class InitializeObject : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public IMapper InstanceMapper = null;

        /// <summary>
        /// 
        /// </summary>
        public InitializeObject()
        {
            // Initialize Auto Map
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfile<MappingProfile>();
            var configuration = new MapperConfiguration(mappings);
            InstanceMapper = configuration.CreateMapper();
        }

        /// <summary>
        /// Initialize Book Repository
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IBookRepository InitializeBookRepository(DemoDbContext context)
        {
            return new BookRepository(context);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            InstanceMapper = null;
        }
    }
}

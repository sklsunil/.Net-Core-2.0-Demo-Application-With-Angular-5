using AutoMapper;
using Demo.DomainModels.Entities;
using Demo.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.XUnitTest
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapping entity with Dto
        /// </summary>
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<DomainModels.Entities.Book, BookModel>();
            CreateMap<BookModel, DomainModels.Entities.Book>();
        }
    }
}

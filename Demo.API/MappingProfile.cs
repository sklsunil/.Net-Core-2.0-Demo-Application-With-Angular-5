using AutoMapper;
using Demo.DomainModels.Entities;
using Demo.DomainModels.Models;
using System;
using System.Globalization;

namespace Demo.API
{
    /// <summary>
    /// Profile mappings
    /// </summary>
    public class MappingProfile : Profile
    {

        /// <summary>
        /// Mapping entity with Dto
        /// </summary>
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();
        }
    }
}

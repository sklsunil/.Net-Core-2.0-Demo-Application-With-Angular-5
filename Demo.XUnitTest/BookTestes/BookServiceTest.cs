using Demo.BLL.Service;
using Demo.DAL.Repositories;
using Demo.DomainModels.ApiResponse;
using Demo.DomainModels.Entities;
using Demo.DomainModels.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.XUnitTest.BookTestes
{
    [Trait("category", "Books Service : UnitTest")]
    [Collection("TestDb")]
    public class BookServiceTest : IClassFixture<InitializeObject>
    {
        DatabaseFixture _fixture;
        private InitializeObject _initialize;
        private BookService _bookService;

        public BookServiceTest(DatabaseFixture fixture, InitializeObject initialize)
        {
            this._fixture = fixture;
            this._initialize = initialize;
            _bookService = new BookService(this._initialize.InitializeBookRepository(this._fixture._context), this._initialize.InstanceMapper);
        }

        [Fact(DisplayName = "Book : Check Ok result From Get All")]
        public async Task GetAllOkResult()
        {
            var result = await _bookService.GetBooksAsync();
            var okResult = result.Should().BeOfType<ApiResponse<List<BookModel>>>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Book : Get All Books")]
        public async Task GetAllBooks()
        {
            var result = await _bookService.GetBooksAsync();
            var okResult = result.Should().BeOfType<ApiResponse<List<BookModel>>>().Subject;
            var books = this._fixture._context.Book;
            okResult.Data.Count.Should().Be(books.Count());
            okResult.Data.FirstOrDefault().Name.Should().Be(books.FirstOrDefault().Name);
        }

        [Fact(DisplayName = "Book : Check Ok result From Get Books by Id.")]
        public async Task GetAllByIdOkResult()
        {
            var books = this._fixture._context.Book;
            var result = await _bookService.GetBookByIdAsync(books.First().Id);
            var okResult = result.Should().BeOfType<ApiResponse<BookModel>>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Book : Get Book By Id")]
        public async Task GetBookById()
        {
            var books = this._fixture._context.Book;
            var result = await _bookService.GetBookByIdAsync(books.First().Id);
            var okResult = result.Should().BeOfType<ApiResponse<BookModel>>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Data.Id.Should().Be(books.First().Id);
            okResult.Data.Name.Should().Be(books.First().Name);
        }

        [Fact(DisplayName = "Book : Add book.")]
        public async Task AddBook()
        {
            BookModel book = new BookModel
            {
                Authors = "Test Case Author",
                Name = "Name Of Test Book",
                NumberOfPages = 10
            };
            var result = await _bookService.AddorUpdateBookAsync(book);
            var okResult = result.Should().BeOfType<ApiResponse<bool>>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Data.Should().Be(true);
        }

        [Fact(DisplayName = "Book : Delete book.")]
        public async Task Delete()
        {
            var book = this._fixture._context.Book.Last();
            var result = await _bookService.DeleteAsync(book.Id);
            var okResult = result.Should().BeOfType<ApiResponse<bool>>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Data.Should().Be(true);
        }
    }
}

using Demo.BLL.IService;
using Demo.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.DomainModels.ApiResponse;
using System.Net;
using Demo.DomainModels.Exceptions;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Demo.DomainModels.Models;
using Demo.DomainModels.Entities;
using System.Threading.Tasks;


namespace Demo.BLL.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            this._bookRepository = bookRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get All Book Async
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<List<BookModel>>> GetBooksAsync()
        {
            try
            {
                var result = await _bookRepository.GetAllAsync();
                var model = _mapper.Map<List<Book>, List<BookModel>>(result);


                return ApiResponse<List<BookModel>>.SuccessResult(model);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<List<BookModel>>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<BookModel>>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<List<BookModel>>.ErrorResult(message: ex.Message);
            }
        }


        /// <summary>
        /// Get Book By Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<BookModel>> GetBookByIdAsync(int id)
        {
            try
            {
                var result = await _bookRepository.GetByIdAsync(id);
                var model = _mapper.Map<Book, BookModel>(result);

                return ApiResponse<BookModel>.SuccessResult(model);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<BookModel>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<BookModel>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<BookModel>.ErrorResult(message: ex.Message);
            }
        }

        /// <summary>
        /// Add or Update Book Async
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> AddorUpdateBookAsync(BookModel model)
        {
            try
            {
                var result = _mapper.Map<BookModel, Book>(model);
                result.DateOfPublication = DateTime.Now;
                if (result.Id > 0)
                {
                    await _bookRepository.UpdateAsync(result);
                }
                else
                {
                    await _bookRepository.AddAsync(result);
                }

                var testResult = await Task.FromResult(true);

                return ApiResponse<bool>.SuccessResult(testResult);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }

        }

        /// <summary>
        /// Delete Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var result = await _bookRepository.DeleteAsync(id);
                return ApiResponse<bool>.SuccessResult(result);
            }
            catch (Exception ex) when (ex is FailException || ex is ValidationException || ex is ArgumentException)
            {
                return ApiResponse<bool>.ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
            catch (Exception ex) when (ex is ErrorException)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
            catch (Exception ex)
            {
                //LoggingManager.Error(ex.ToString());
                return ApiResponse<bool>.ErrorResult(message: ex.Message);
            }
        }
    }
}

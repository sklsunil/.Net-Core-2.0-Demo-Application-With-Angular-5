using System;
using System.Collections.Generic;
using System.Text;
using Demo.DomainModels.ApiResponse;
using Demo.DomainModels.Models;
using System.Threading.Tasks;

namespace Demo.BLL.IService
{
    public interface IBookService
    {
        Task<ApiResponse<List<BookModel>>> GetBooksAsync();
        Task<ApiResponse<BookModel>> GetBookByIdAsync(int id);
        Task<ApiResponse<bool>> AddorUpdateBookAsync(BookModel model);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}

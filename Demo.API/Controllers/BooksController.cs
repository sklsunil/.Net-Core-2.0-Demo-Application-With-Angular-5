using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.BLL.IService;
using Swashbuckle.AspNetCore.SwaggerGen;
using Demo.DomainModels.ApiResponse;
using Demo.DomainModels.Models;
using Microsoft.AspNetCore.Cors;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authorization;
using Demo.DomainModels.Role;

namespace Demo.API.Controllers
{
    /// <summary>
    /// Books Controller
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bookService"></param>
        public BooksController(IBookService bookService)
        {
            this._bookService = bookService;
        }
        #endregion


        #region Action Methods

        /// <summary>
        /// GET api/books
        /// </summary>      
        /// <remarks>
        /// Sample request:
        ///  1. GET: /api/books      
        /// </remarks>      
        /// <returns>It returns list of book.</returns>
        /// <response code="200">It returns list of book.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesResponseType(typeof(ApiResponse<List<BookModel>>), 200)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _bookService.GetBooksAsync();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// GET api/books/5
        /// </summary>      
        /// <remarks>
        /// Sample request:
        ///  1. GET: /api/books/5     
        /// </remarks>      
        /// <returns>It returns true in case of suceess.</returns>
        /// <response code="200">It returns book.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesResponseType(typeof(ApiResponse<List<BookModel>>), 200)]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// GET api/books
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  1. GET: /api/books
        ///  2. 
        ///      {
        ///      "id": 0,
        ///      "name": "string",
        ///      "authors": "string",
        ///      "numberOfPages": 0,
        ///      "dateOfPublication": "2018-03-30T15:05:35.072Z"
        ///      }
        /// </remarks> 
        /// <returns>It returns true in case of suceess.</returns>
        /// <response code="200">It returns book.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<ActionResult> Put([FromBody]BookModel model)
        {
            var result = await _bookService.AddorUpdateBookAsync(model);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// GET api/books
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  1. GET: /api/books
        ///  2. 
        ///      {
        ///      "id": 1,
        ///      "name": "string",
        ///      "authors": "string",
        ///      "numberOfPages": 0,
        ///      "dateOfPublication": "2018-03-30T15:05:35.072Z"
        ///      }
        /// </remarks> 
        /// <returns>It returns true in case of suceess.</returns>
        /// <response code="200">It returns book.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<ActionResult> Post([FromBody]BookModel model)
        {
            model.Id = 0;
            var result = await _bookService.AddorUpdateBookAsync(model);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// GET api/books/5
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///  1. GET: /api/books/5        
        /// </remarks> 
        /// <returns>It returns true in case of suceess.</returns>
        /// <response code="200">It returns book.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal Server Error.</response> 
        /// <returns>It returns true in case of suceess.</returns>
        [HttpDelete("id")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var book = await _bookService.DeleteAsync(id);
            return Ok();
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.BLL.IService;
using Demo.DAL;
using Demo.DomainModels.ApiResponse;
using Demo.DomainModels.Entities;
using Demo.DomainModels.Extensions;
using Demo.DomainModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AccountController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly IUnitOfWork _uow;
        private readonly IAntiForgeryCookieService _antiforgery;

        public AccountController(
            IUsersService usersService,
            ITokenStoreService tokenStoreService,
            IUnitOfWork uow,
            IAntiForgeryCookieService antiforgery)
        {
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));

            _tokenStoreService = tokenStoreService;
            _tokenStoreService.CheckArgumentIsNull(nameof(tokenStoreService));

            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _antiforgery = antiforgery;
            _antiforgery.CheckArgumentIsNull(nameof(antiforgery));
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]  LoginUser loginUser)
        {
            if (loginUser == null)
            {

                return new OkObjectResult(ApiResponse<string>.ErrorResult(message: "user is not set.", statusCode: HttpStatusCode.BadRequest));
            }

            var user = await _usersService.FindUserAsync(loginUser.Email, loginUser.Password);
            if (user == null || !user.IsActive)
            {
                return new OkObjectResult(ApiResponse<string>.ErrorResult(message: "Invalid login and Password.", statusCode: HttpStatusCode.Unauthorized));
            }

            var (accessToken, refreshToken, claims) = await _tokenStoreService.CreateJwtTokens(user, refreshTokenSource: null);

            _antiforgery.RegenerateAntiForgeryCookies(claims);

            return new OkObjectResult(ApiResponse<object>.SuccessResult(new { access_token = accessToken, refresh_token = refreshToken }));


        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshToken(string refresh_token)
        {

            if (string.IsNullOrWhiteSpace(refresh_token))
            {
                return new OkObjectResult(ApiResponse<string>.ErrorResult(message: "refreshToken is not set.", statusCode: HttpStatusCode.BadRequest));

            }

            var token = await _tokenStoreService.FindTokenAsync(refresh_token);
            if (token == null)
            {
                return new OkObjectResult(ApiResponse<string>.ErrorResult(message: "Unauthorized.", statusCode: HttpStatusCode.Unauthorized));
            }

            var (accessToken, newRefreshToken, claims) = await _tokenStoreService.CreateJwtTokens(token.User, refresh_token);

            _antiforgery.RegenerateAntiForgeryCookies(claims);

            return new OkObjectResult(ApiResponse<object>.SuccessResult(new { access_token = accessToken, refresh_token = newRefreshToken }));

        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<bool> Logout(string refreshToken)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            await _tokenStoreService.RevokeUserBearerTokensAsync(userIdValue, refreshToken);
            await _uow.SaveChangesAsync();

            _antiforgery.DeleteAntiForgeryCookies();

            return true;
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return Json(new { Username = claimsIdentity.Name });
        }
    }
}

using Demo.BLL.IService;
using Demo.DAL;
using Demo.DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Demo.DomainModels.Extensions;
using Microsoft.AspNetCore.Http;
using Demo.DomainModels.Models;
using AutoMapper;
using Demo.DomainModels.Role;
using Demo.DomainModels.Exceptions;

namespace Demo.BLL.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<User> _users;
        private readonly DbSet<UserRole> _userRoles;
        private readonly DbSet<Role> _roles;
        private readonly ISecurityService _securityService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public UsersService(
            IUnitOfWork uow,
            ISecurityService securityService,
            IHttpContextAccessor contextAccessor,
            IMapper mapper)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _users = _uow.Set<User>();
            _userRoles = _uow.Set<UserRole>();
            _roles = _uow.Set<Role>();

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));

            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));

            _mapper = mapper;
            _mapper.CheckArgumentIsNull(nameof(_mapper));
        }

        public Task<User> FindUserAsync(int userId)
        {
            return _users.FindAsync(userId);
        }

        public Task<User> FindUserAsync(string email)
        {
            return _users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> AddUser(AppUser user)
        {
            var roleExistInDb = await _roles.FirstOrDefaultAsync(x => x.Name.ToLower() == user.Role.ConvertToString().ToLower());
            if (roleExistInDb == null)
            {
                throw new FailException("Role not exist in database.");
            }
            var model = _mapper.Map<AppUser, User>(user);
            model.Id = 0;
            model.Password = _securityService.GetSha256Hash(user.Password);
            _users.Add(model);
            try
            {
                await _uow.SaveChangesAsync();
                _userRoles.Add(new UserRole { UserId = model.Id, RoleId = roleExistInDb.Id });
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (model.Id > 0)
                {
                    var userDbExist = await _users.FindAsync(model.Id);
                    _users.Remove(userDbExist);
                    await _uow.SaveChangesAsync();
                }
                throw new FailException(ex.Message);
            }
            return model;
        }

        public async Task<User> FindUserAsync(string email, string password)
        {
            var passwordHash = _securityService.GetSha256Hash(password);
            var user = await _users.FirstOrDefaultAsync(x => x.Email == email && x.Password == passwordHash);
           
            return user;
        }

        public async Task<string> GetSerialNumberAsync(int userId)
        {
            var user = await FindUserAsync(userId);
            return user.SerialNumber;
        }

        public async Task UpdateUserLastActivityDateAsync(int userId)
        {
            var user = await FindUserAsync(userId);
            if (user.LastLoggedIn != null)
            {
                var updateLastActivityDate = TimeSpan.FromMinutes(2);
                var currentUtc = DateTimeOffset.UtcNow;
                var timeElapsed = currentUtc.Subtract(user.LastLoggedIn.Value);
                if (timeElapsed < updateLastActivityDate)
                {
                    return;
                }
            }
            user.LastLoggedIn = DateTimeOffset.UtcNow;
            await _uow.SaveChangesAsync();
        }

        public int GetCurrentUserId()
        {
            var claimsIdentity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.UserData);
            var userId = userDataClaim?.Value;
            return string.IsNullOrWhiteSpace(userId) ? 0 : int.Parse(userId);
        }

        public Task<User> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return FindUserAsync(userId);
        }

        public async Task<(bool Succeeded, string Error)> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var currentPasswordHash = _securityService.GetSha256Hash(currentPassword);
            if (user.Password != currentPasswordHash)
            {
                return (false, "Current password is wrong.");
            }

            user.Password = _securityService.GetSha256Hash(newPassword);
            // user.SerialNumber = Guid.NewGuid().ToString("N"); // To force other logins to expire.
            await _uow.SaveChangesAsync();
            return (true, string.Empty);
        }
    }
}

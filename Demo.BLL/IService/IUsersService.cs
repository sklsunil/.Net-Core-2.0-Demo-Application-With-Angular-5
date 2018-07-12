using Demo.DomainModels.Entities;
using Demo.DomainModels.Models;
using Demo.DomainModels.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.IService
{
    public interface IUsersService
    {
        Task<string> GetSerialNumberAsync(int userId);
        Task<User> FindUserAsync(string username, string password);
        Task<User> FindUserAsync(int userId);
        Task<User> FindUserAsync(string email);
        Task UpdateUserLastActivityDateAsync(int userId);
        Task<User> GetCurrentUserAsync();
        int GetCurrentUserId();
        Task<(bool Succeeded, string Error)> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<User> AddUser(AppUser user);
    }
}

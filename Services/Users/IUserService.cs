using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Users;

namespace Services.Users
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<UserValidateModel> ValidateUserAsync(string email, string password);
    }
}

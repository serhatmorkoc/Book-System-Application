using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Entities;
using Data.Models.Users;

namespace Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        #region Ctor

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Methods

        public async Task<User> GetUserByIdAsync(int userId)
        {
            if (userId == 0)
                return null;

            var result = await _userRepository.GetByIdAsync(userId);
            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = await _userRepository.TableAsync();
            var result = query.FirstOrDefault(x => x.Email == email);

            return result;

        }

        public async Task<UserValidateModel> ValidateUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            if (user == null)
                return UserValidateModel.NotRegistered;

            if (user.Deleted)
                return UserValidateModel.Deleted;

            if (!user.Active)
                return UserValidateModel.NotActive;

            if (user.Password == password)
                return UserValidateModel.Successful;

            if (user.Password != password)
                return UserValidateModel.WrongPassword;

            return UserValidateModel.NotRegistered;
        }

        #endregion

    }
}

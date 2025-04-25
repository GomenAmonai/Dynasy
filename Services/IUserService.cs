using Dynasy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynasy.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task<User> RegisterUserAsync(string name, string email, string password);
        Task<bool> AuthenticateUserAsync(string email, string password);
    }
}
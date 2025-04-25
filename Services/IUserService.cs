using Dynasy.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynasy.Services
{
    public interface IUserService
    {
        Task<UserDTO> RegisterUserAsync(CreateUserDTO createUserDTO);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<bool> AuthenticateUserAsync(string email, string password);
        Task<UserDTO> GetUserByEmailAsync(string email);
    }
}
using Dynasy.Data;
using Dynasy.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net; // Это необходимо для использования BCrypt

namespace Dynasy.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                                 .Include(u => u.Orders) // Если нужно загрузить связанные заказы
                                 .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password)
        {
            var existingUser = await _context.Users
                                             .FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                return null; // Пользователь с таким email уже существует
            }

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password) // Хэшируем пароль
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash); // Сравниваем пароли
        }
    }
}
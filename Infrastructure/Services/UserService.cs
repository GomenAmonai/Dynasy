using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dynasy.Data;
using Dynasy.Models;
using Dynasy.Services;
using Microsoft.EntityFrameworkCore;

namespace Dynasy.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper; // Для маппинга DTO

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Метод для получения всех пользователей
    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<List<UserDTO>>(users); // Маппим сущности в DTO
    }

    // Метод для регистрации пользователя
    public async Task<UserDTO> RegisterUserAsync(CreateUserDTO createUserDTO)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == createUserDTO.Email);
        
        if (existingUser != null)
        {
            return null; // Пользователь с таким email уже существует
        }

        var user = new User
        {
            Name = createUserDTO.Name,
            Email = createUserDTO.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDTO>(user); // Возвращаем DTO после добавления
    }

    // Метод для получения пользователя по Id
    public async Task<UserDTO> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return null;

        return _mapper.Map<UserDTO>(user); // Маппим сущность в DTO
    }

    // Метод для аутентификации пользователя
    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash); // Проверка пароля
    }
}

public class UserDTO
{
}
public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        AppDbContext context,
        IMapper mapper,
        ILogger<UserService> logger,
        IConfiguration configuration,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> RegisterAsync(CreateUserDto createUserDto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == createUserDto.Email);

        if (existingUser != null)
            throw new ValidationException("Пользователь с таким email уже существует");

        var user = _mapper.Map<User>(createUserDto);
        user.CreatedAt = DateTime.UtcNow;
        user.Role = "User"; // По умолчанию роль User
        user.PasswordHash = _passwordHasher.HashPassword(user, createUserDto.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
            throw new ValidationException("Неверный email или пароль");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new ValidationException("Неверный email или пароль");

        var token = GenerateJwtToken(user);
        return token;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        
        if (user == null)
            throw new NotFoundException($"Пользователь с ID {id} не найден");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        
        if (user == null)
            throw new NotFoundException($"Пользователь с ID {id} не найден");

        if (!string.IsNullOrEmpty(updateUserDto.Email) && 
            await _context.Users.AnyAsync(u => u.Email == updateUserDto.Email && u.Id != id))
        {
            throw new ValidationException("Пользователь с таким email уже существует");
        }

        _mapper.Map(updateUserDto, user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
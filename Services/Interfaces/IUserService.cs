public interface IUserService
{
    Task<UserDto> RegisterAsync(CreateUserDto createUserDto);
    Task<string> LoginAsync(LoginDto loginDto);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
}
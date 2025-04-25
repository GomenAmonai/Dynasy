namespace Dynasy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Регистрация пользователя
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser(CreateUserDTO createUserDTO)
        {
            var user = await _userService.RegisterUserAsync(createUserDTO);

            if (user == null)
                return BadRequest("User already exists");

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // Получение всех пользователей
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Получение пользователя по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Аутентификация пользователя
        [HttpPost("authenticate")]
        public async Task<ActionResult> AuthenticateUser(AuthenticateUserDTO authenticateUserDTO)
        {
            var isAuthenticated = await _userService.AuthenticateUserAsync(authenticateUserDTO.Email, authenticateUserDTO.Password);

            if (!isAuthenticated)
                return Unauthorized();

            return Ok();
        }

        // Обновление пользователя
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, updateUserDTO);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }
    }
}
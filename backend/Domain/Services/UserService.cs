using Interfaces;
using Interfaces.IRepos;
using Logic.Entities;
using Logic.ExtensionMethods;

namespace Logic.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(dto => dto.toUser());
        }

        public async Task<User?> Login(string email, string password)
        {
            User? user = null;

            UserDto? dto = await _userRepository.GetUserByEmail(email);

            if (dto != null)
            {
                string hashedPassword = PasswordHasher.HashPassword(password);

                if (hashedPassword == dto.Password)
                {
                    user = new User(dto.UserId, dto.FirstName, dto.LastName, dto.Email);
                }
            }
            return user;
        }

        public async Task<bool> UserExistsByEmailASync(string email)
        {
            UserDto? dto = await _userRepository.GetUserByEmail(email);

            return dto != null;
        }

        public async Task<Guid?> CreateUserAsync(User user)
        {
            UserDto dto = user.toDto();

            Guid? userId = await _userRepository.AddUserAsync(dto);

            return userId;
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdASync(userId);
        }


    }
}

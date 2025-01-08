using bookifyWEBApi.ExportModels;
using bookifyWEBApi.ExtensionsMethods;
using bookifyWEBApi.ImportModels;
using Logic.Entities;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookifyWEBApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public UserController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEx>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                var usersEx = users.Select(user => new UserEx()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });

                return Ok(usersEx);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequestIm registrationRequestIM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // validation errors
            }

            if (registrationRequestIM == null)
            {
                return BadRequest("User input model cannot be null.");
            }

            bool userExists = await _userService.UserExistsByEmailASync(registrationRequestIM.Email);

            if (userExists)
            {
                return Conflict(new { Message = "A user with this email already exists." });
            }

            User user = registrationRequestIM.ToUser();

            try
            {
                Guid? registeredUser = await _userService.CreateUserAsync(user);
                return Ok(new { registeredUser, Message = "User created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestIm loginRequestIM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // validation errors
            }

            try
            {
                User? user = await _userService.Login(loginRequestIM.Email, loginRequestIM.Password); // NULL if login failed.
                if (user == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                UserEx userEx = new UserEx()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

                string token = _jwtService.GenerateToken(user.UserId);

                return Ok(new { Token = token, User = userEx });
            }
            catch
            {
                return Unauthorized("Invalid credentials");
            }
        }
    }
}

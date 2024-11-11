using bookifyWEBApi.ExportModels;
using bookifyWEBApi.ExtensionsMethods;
using bookifyWEBApi.ImportModels;
using Logic.Entities;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookifyWEBApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        public UserController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequestIM registrationRequestIM)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); } //validation errors


            if (registrationRequestIM == null) { return BadRequest("User input model cannot be null."); }

            bool userExists = await _userService.UserExistsByEmailASync(registrationRequestIM.Email);

            if (userExists)
            {
                return Conflict(new { Message = "A user with this email already exists." });
            };

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
        public async Task<ActionResult> Login([FromBody] LoginRequestIM loginRequestIM)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); } //validation errors

            try
            {
                User? user = await _userService.Login(loginRequestIM.Email, loginRequestIM.Password); //NULL if login failed.
                UserEx userEx = new UserEx()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

                if (user != null)
                {
                    string token = _jwtService.GenerateToken(user.UserId);

                    return Ok(new { Token = token, User = userEx });
                }

                return Unauthorized("Invalid credentials");
            }
            catch
            {
                return Unauthorized("Invalid credentials");
            }
        }
    }
}

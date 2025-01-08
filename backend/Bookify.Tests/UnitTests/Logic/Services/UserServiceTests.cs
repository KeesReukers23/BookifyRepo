using Interfaces;
using Interfaces.IRepos;
using Logic;
using Logic.Entities;
using Logic.ExtensionMethods;
using Logic.Services;
using Moq;

namespace Bookify.Tests.UnitTests.Logic.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly JwtService _jwtService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _jwtService = new JwtService("your-secret-key", "your-issuer", "your-audience");
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            string email = "johndoe@gmail.com";
            string password = "password123";
            UserDto expectedUserDto = new UserDto()
            {
                UserId = new Guid(),
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = PasswordHasher.HashPassword(password)
            };


            _mockUserRepository.Setup(repo => repo.GetUserByEmail(email))
                                .ReturnsAsync(expectedUserDto);

            //Act 
            var result = await _userService.Login(email, password);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUserDto.UserId, result.UserId);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string email = "johndoe@gmail.com";
            string password = "password123";
            UserDto expectedUserDto = new UserDto()
            {
                UserId = new Guid(),
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Password = PasswordHasher.HashPassword(password)
            };

            _mockUserRepository.Setup(repo => repo.GetUserByEmail(email))
                               .ReturnsAsync(expectedUserDto);

            //Act
            var result = await _userService.Login(email, "wrongpassword");


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_UserNotFound_ReturnsNull()
        {

            // Arrange
            string email = "nonexisting@gmail.com";
            string password = "anyPassword";


            _mockUserRepository.Setup(repo => repo.GetUserByEmail(email))
                               .ReturnsAsync((UserDto?)null);

            // Act
            var result = await _userService.Login(email, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UserExistsByEmailAsync_UserExists_ReturnsTrue()
        {
            // Arrange
            string email = "existinguser@example.com";

            _mockUserRepository.Setup(repo => repo.GetUserByEmail(email))
                               .ReturnsAsync(new UserDto()
                               {
                                   UserId = Guid.NewGuid(),
                                   FirstName = "John",
                                   LastName = "Doe",
                                   Email = email,
                                   Password = "hashedPassword"
                               });

            // Act
            var result = await _userService.UserExistsByEmailASync(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UserExistsByEmailAsync_UserDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string email = "nonexistinguser@example.com";

            _mockUserRepository.Setup(repo => repo.GetUserByEmail(email))
                               .ReturnsAsync((UserDto?)null);

            // Act
            var result = await _userService.UserExistsByEmailASync(email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CreateUserAsync_Success_ReturnsUserId()
        {
            // Arrange
            var user = new User("John", "Doe", "johndoe@gmail.com", "password");

            Guid expectedGuid = user.UserId;

            UserDto dto = user.toDto();

            _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<UserDto>()))
                               .ReturnsAsync(expectedGuid);
            // Act
            var result = await _userService.CreateUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGuid, result);
            _mockUserRepository.Verify(repo => repo.AddUserAsync(It.IsAny<UserDto>()), Times.Once);  // Verifieer dat AddUserAsync eenmaal is aangeroepen
        }

        [Fact]
        public async Task CreateUserAsync_Failure_ReturnsNull()
        {
            // Arrange
            var user = new User("John", "Doe", "johndoe@gmail.com", "password");

            var userDto = user.toDto();

            // Mock de AddUserAsync-methode die faalt (bijvoorbeeld databasefout)
            _mockUserRepository.Setup(repo => repo.AddUserAsync(userDto))
                               .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var result = await _userService.CreateUserAsync(user);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserExists_ReturnsUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new UserDto
            {
                UserId = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByIdASync(userId))
                           .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.UserId, result?.UserId);
            Assert.Equal(expectedUser.FirstName, result?.FirstName);
            Assert.Equal(expectedUser.LastName, result?.LastName);
            Assert.Equal(expectedUser.Email, result?.Email);

            _mockUserRepository.Verify(repo => repo.GetUserByIdASync(userId), Times.Once);
        }
        [Fact]
        public async Task GetUserByIdAsync_UserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserRepository.Setup(repo => repo.GetUserByIdASync(userId))
                               .ReturnsAsync((UserDto?)null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
            _mockUserRepository.Verify(repo => repo.GetUserByIdASync(userId), Times.Once);
        }

    }
}

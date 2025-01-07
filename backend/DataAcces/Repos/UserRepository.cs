using DataAcces;
using Interfaces;
using Interfaces.IRepos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly BookifyContext _context;

        public UserRepository(BookifyContext context)
        {
            _context = context;
        }

        public async Task<Guid?> AddUserAsync(UserDto userDto)
        {
            try
            {
                await _context.Users.AddAsync(userDto);
                await _context.SaveChangesAsync();
                return userDto.UserId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserDto?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserDto?> GetUserByIdASync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<UserDto?> GetUserByIdASync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}

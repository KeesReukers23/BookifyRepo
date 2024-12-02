

namespace Interfaces.IRepos
{
    public interface IUserRepository
    {
        Task<Guid?> AddUserAsync(UserDto userDto);
        Task<UserDto?> GetUserByIdASync(Guid userId);
        Task<UserDto?> GetUserByEmail(string email);
        Task DeleteUserAsync(Guid userId);


    }
}
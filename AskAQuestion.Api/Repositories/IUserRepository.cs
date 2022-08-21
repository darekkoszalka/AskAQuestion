using AskAQuestion.Api.Entities;

namespace AskAQuestion.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);
        Task DeleteUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task UpdateUser(User user);
        Task<bool> UserExists(Guid id);
        Task CreateUserRole(UserRole userRole);
        Task<User> GetUserByEmail(string email);
    }
}
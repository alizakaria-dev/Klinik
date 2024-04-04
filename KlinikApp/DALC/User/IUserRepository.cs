using Shared.Models;

namespace DALC.User
{
    public interface IUserRepository
    {
        public Task<List<Shared.Models.User>> GetAllUsers();
        public Task<Shared.Models.User> AddUser(Shared.Models.User user);
        public Task<Shared.Models.User> UpdateUser(Shared.Models.User user);
        public Task DeleteUser(int id);
        public Task<Shared.Models.User> Login(Login login);
    }
}

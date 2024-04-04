using Shared.Models;

namespace DALC.Role
{
    public interface IRoleRepository
    {
        public Task<List<Shared.Models.Role>> GetAllRoles();
        public Task<Shared.Models.Role> AddRole(Shared.Models.Role role);
        public Task<Shared.Models.Role> UpdateRole(Shared.Models.Role role);
        public Task DeleteRole(int id);
    }
}

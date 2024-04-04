using DALC.Context;
using Dapper;
using System.Data;

namespace DALC.Role
{
    public class RoleRepository : IRoleRepository
    {
        private DapperContext _context;

        public RoleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.Role> AddRole(Shared.Models.Role role)
        {
            var procedure = "ADDROLE";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {
                parameters.Add("ROLENAME", role.ROLENAME, DbType.String);
                parameters.Add("ROLEID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                var roleId = parameters.Get<int>("ROLEID");
                var createdRole = new Shared.Models.Role
                {
                    ROLEID = roleId,
                    ROLENAME = role.ROLENAME,
                };
                return createdRole;
            }
        }

        public async Task DeleteRole(int id)
        {

            var procedure = "DELETEROLE";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {
                parameters.Add("ROLEID", id, DbType.Int32);
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

        }

        public async Task<List<Shared.Models.Role>> GetAllRoles()
        {

            var procedure = "GETROLES";

            using (var connection = _context.CreateConnection())
            {
                var roles = await connection.QueryAsync<Shared.Models.Role>(procedure, commandType: CommandType.StoredProcedure);

                var rolesList = roles.ToList();

                return rolesList;
            }

        }

        public async Task<Shared.Models.Role> UpdateRole(Shared.Models.Role role)
        {

            var procedure = "UPDATEROLE";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {
                parameters.Add("ROLENAME", role.ROLENAME, DbType.String);
                parameters.Add("ROLEID", role.ROLEID, DbType.Int32);
                var updatedRole = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return role;

        }
    }
}

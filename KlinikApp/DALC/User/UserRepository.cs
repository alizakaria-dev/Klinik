using DALC.Context;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALC.User
{
    public class UserRepository : IUserRepository
    {
        private DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.User> AddUser(Shared.Models.User user)
        {
            var procedure = "ADDUSER";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {

                parameters.Add("USERNAME", user.USERNAME, DbType.String);
                parameters.Add("PASSWORD", user.PASSWORD, DbType.String);
                parameters.Add("ROLEID", user.ROLEID, DbType.Int32);
                parameters.Add("USERID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                var userId = parameters.Get<int>("USERID");
                var createdUser = new Shared.Models.User
                {
                    USERID = userId,
                    USERNAME = user.USERNAME,
                    ROLEID = user.ROLEID
                };
                return createdUser;
            }
        }

        public async Task DeleteUser(int id)
        {

            var procedure = "DELETEUSER";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {
                parameters.Add("USERID", id, DbType.Int32);
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

        }

        public async Task<List<Shared.Models.User>> GetAllUsers()
        {

            var procedure = "GETUSERS";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<Shared.Models.User>(procedure, commandType: CommandType.StoredProcedure);

                var usersList = users.ToList();

                return usersList;
            }

        }

        public async Task<Shared.Models.User> Login(Login login)
        {
            var procedure = "USERLOGIN";

            var parameters = new DynamicParameters();

            using(var connection = _context.CreateConnection())
            {
                parameters.Add("USERNAME", login.USERNAME, dbType: DbType.String);
                parameters.Add("PASSWORD", login.PASSWORD, dbType: DbType.String);

                var user = await connection.QueryFirstOrDefaultAsync<Shared.Models.User>(procedure,parameters,commandType: CommandType.StoredProcedure);

                return user;
            }
        }

        public async Task<Shared.Models.User> UpdateUser(Shared.Models.User user)
        {

            var procedure = "UPDATEUSER";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {

                parameters.Add("USERNAME", user.USERNAME, DbType.String);
                parameters.Add("PASSWORD", user.PASSWORD, DbType.String);
                parameters.Add("ROLEID", user.ROLEID, DbType.Int32);
                parameters.Add("USERID", user.USERID, dbType: DbType.Int32);
                var updatedUser = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return user;

        }
    }
}

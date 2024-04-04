using DALC.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using Shared.Extensions;
using Shared.Jwt;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;

namespace BLC.User
{
    public class UserManager
    {
        private IUserRepository _repository;

        private Jwt _jwt;

        public UserManager(IUserRepository repository,Jwt jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        public async Task<Result> GetAllUsers()
        {
            try
            {
                var users = await _repository.GetAllUsers();

                if (users == null || !users.Any())
                {
                    return Result.Ok("No users were found", 404);
                }

                return Result.Ok(users);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> AddUser(Shared.Models.User user)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if(user.ROLEID == null)
                    {
                        user.ROLEID = Constants.userRole;
                    }

                    user.PASSWORD = user.PASSWORD.Encrypt();

                    var createdUser = await _repository.AddUser(user);

                    var token = _jwt.GenerateToken(createdUser.USERNAME, createdUser.ROLEID.Value, createdUser.USERID);

                    createdUser.Token = token;

                    oScope.Complete();

                    return Result.Ok(createdUser);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UpdateUser(Shared.Models.User user)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var updatedUser = await _repository.UpdateUser(user);

                    oScope.Complete();

                    return Result.Ok(updatedUser);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteUser(int id)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteUser(id);

                    oScope.Complete();

                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> Login(Login login)
        {
            try
            {
                login.PASSWORD = login.PASSWORD.Encrypt();

                var user = await _repository.Login(login);

                if(user == null)
                {
                    return Result.Ok("Username or password incorrect", 404);
                }

                var token = _jwt.GenerateToken(user.USERNAME, user.ROLEID.Value, user.USERID);

                user.Token = token;

                return Result.Ok(user);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        //public string GenerateToken(string userName, int roleId, int userId)
        //{
        //    var claims = new Claim[]
        //    {
        //        new Claim("UserId", userId.ToString()),
        //        new Claim(JwtRegisteredClaimNames.Name, userName),
        //        new Claim("Role",roleId.ToString())
        //    };

        //    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey
        //        (System.Text.Encoding.UTF8.GetBytes(_jwtConfig.SecretKey)),
        //        SecurityAlgorithms.HmacSha256
        //        );

        //    var token = new JwtSecurityToken(_jwtConfig.Issuer, _jwtConfig.Audience, claims, null, DateTime.UtcNow.AddHours(1), signingCredentials);

        //    string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        //    return tokenValue;
        //}
    }
}

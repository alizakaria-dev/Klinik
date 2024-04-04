using DALC.Role;
using Shared.Models;
using System.Transactions;

namespace BLC.Role
{
    public class RoleManager
    {
        private IRoleRepository _repository;

        public RoleManager(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> GetAllRoles()
        {
            try
            {
                var roles = await _repository.GetAllRoles();

                if (roles == null || !roles.Any())
                {
                    return Result.Ok("No roles were found", 404);
                }

                return Result.Ok(roles);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> AddRole(Shared.Models.Role role)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdRole = await _repository.AddRole(role);

                    oScope.Complete();

                    return Result.Ok(createdRole);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UpdateRole(Shared.Models.Role role)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var updatedRole = await _repository.UpdateRole(role);

                    oScope.Complete();

                    return Result.Ok(updatedRole);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteRole(int id)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteRole(id);

                    oScope.Complete();

                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }
    }
}

using BLC.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.PermissionRules;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RoleManager _manager;

        public RolesController(RoleManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _manager.GetAllRoles();

            return Ok(roles);
        }


        [HttpPut]
        [Route("UpdateRole")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UpdateRole(Shared.Models.Role role)
        {
            var updatedRole = await _manager.UpdateRole(role);

            return Ok(updatedRole);
        }

        [HttpPost]
        [Route("AddRole")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> AddRole(Shared.Models.Role role)
        {
            var addedRole = await _manager.AddRole(role);

            return Ok(addedRole);
        }

        [HttpDelete]
        [Route("DeleteRole")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _manager.DeleteRole(id);

            return Ok(result);
        }
    }
}

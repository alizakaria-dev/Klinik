using BLC.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Models;
using Shared.PermissionRules;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager _manager;

        public UsersController(UserManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [PermissionRule(Constants.adminRole, Constants.secretaryRole)]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _manager.GetAllUsers();

            return Ok(users);
        }


        [HttpPut]
        [Route("UpdateUser")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UpdateUser(Shared.Models.User user)
        {
            var updatedUser = await _manager.UpdateUser(user);

            return Ok(updatedUser);
        }

        [HttpPost]
        [Route("AddUser")]
        [PermissionRule(Constants.adminRole, Constants.userRole,Constants.secretaryRole)]
        public async Task<IActionResult> AddUser(Shared.Models.User user)
        {
            var addedUser = await _manager.AddUser(user);

            return Ok(addedUser);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _manager.Login(login);

            return Ok(user);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _manager.DeleteUser(id);

            return Ok(result);
        }
    }
}

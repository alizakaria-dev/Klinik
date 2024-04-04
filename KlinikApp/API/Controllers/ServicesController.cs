using BLC.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Models;
using Shared.PermissionRules;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private ServiceManager _manager;
        public ServicesController(ServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _manager.GetAllServices();
            return Ok(services);
        }

        [HttpGet]
        [Route("GetServiceById")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _manager.GetServiceById(id);
            return Ok(service);
        }

        [HttpPost]
        [Route("CreateService")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> CreateService(Service service)
        {
            var createdService = await _manager.CreateService(service);
            return Ok(createdService);
        }

        [HttpPut]
        [Route("UpdateService")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UpdateService(Service service)
        {
            var UpdatedService = await _manager.UpdateService(service);
            return Ok(UpdatedService);
        }

        [HttpDelete]
        [Route("DeleteService")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteService(int id)
        {
            var deletedService = await _manager.DeleteService(id);
            return Ok(deletedService);
        }
    }
}

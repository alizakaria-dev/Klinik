using BLC.Info;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.PermissionRules;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private InfoManager _manager;

        public InfoController(InfoManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetInfo")]
        public async Task<IActionResult> GetInfo()
        {
            var info = await _manager.GetInfo();

            return Ok(info);
        }

        [HttpDelete]
        [Route("DeleteInfo")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteInfo(int id)
        {
            var result = await _manager.DeleteInfo(id);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateInfo")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UpdateInfo(Shared.Models.Info info)
        {
            var udpatedInfo = await _manager.UpdateInfo(info);

            return Ok(udpatedInfo);
        }

        [HttpPost]
        [Route("CreateInfo")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> CreateInfo(Shared.Models.Info info)
        {
            var addedInfo = await _manager.CreateInfo(info);

            return Ok(addedInfo);
        }

        [HttpPost]
        [Route("UploadInfoFile")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UploadInfoFile(IFormFileCollection files)
        {
            var uploadedFiles = await _manager.UploadInfoFile(files);

            return Ok(uploadedFiles);
        }
    }
}

using BLC.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.PermissionRules;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private FileManager _manager;

        public FilesController(FileManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("AddFile")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> AddFile(Shared.Models.File file)
        {
            var createdFile = await _manager.AddFile(file);

            return Ok(createdFile);
        }

        [HttpDelete]
        [Route("DeleteFile")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var result = await _manager.DeleteFile(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetRelatedFiles")]
        public async Task<IActionResult> GetRelatedFiles(string relField, string relTable, int relKey)
        {
            var result = await _manager.GetRelatedFiles(relField, relTable, relKey);

            return Ok(result);
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(int? relKey, string relTable, string relField)
        {
            var files = HttpContext.Request.Form.Files;
            var result = await _manager.UploadFile(files,relKey,relTable,relField);

            return Ok(result);
        }
    }
}

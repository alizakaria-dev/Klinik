using BLC.FeatureItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.PermissionRules;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureItemsController : ControllerBase
    {
        private FeatureItemManager _manager;

        public FeatureItemsController(FeatureItemManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllFeatureItems")]
        public async Task<IActionResult> GetAllFeatureItems()
        {
            var featureItems = await _manager.GetAllFeatureItems();

            return Ok(featureItems);
        }

        [HttpPost]
        [Route("CreateFeatureItem")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> CreateFeatureItem(Shared.Models.FeatureItem featureItem)
        {
            var createdFeatureItem = await _manager.CreateFeatureItem(featureItem);

            return Ok(createdFeatureItem);
        }

        [HttpPut]
        [Route("UpdateFeatureItem")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UpdateFeatureItem(Shared.Models.FeatureItem featureItem)
        {
            var updatedFeatureItem = await _manager.UpdateFeatureItem(featureItem);

            return Ok(updatedFeatureItem);
        }

        [HttpDelete]
        [Route("DeleteFeatureItem")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteFeatureItem(int id)
        {
            var result = await _manager.DeleteFeatureItem(id);

            return Ok(result);
        }
    }
}

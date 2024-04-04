using BLC.Feature;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.PermissionRules;
using Shared.Constants;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private FeatureManager _manager;

        public FeaturesController(FeatureManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetFeature")]
        public async Task<IActionResult> GetFeature()
        {
            var feature = await _manager.GetFeature();

            return Ok(feature);
        }

        [HttpPost]
        [Route("CreateFeature")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> CreateFeature(Shared.Models.Feature feature)
        {
            var createdFeature = await _manager.CreateFeature(feature);

            return Ok(createdFeature);
        }

        [HttpPut]
        [Route("UpdateFeature")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UpdateFeature(Shared.Models.Feature feature)
        {
            var updatedFeature = await _manager.UpdateFeature(feature);

            return Ok(updatedFeature);
        }

        [HttpDelete]
        [Route("DeleteFeature")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var result = await _manager.DeleteFeature(id);

            return Ok(result);
        }
    }
}

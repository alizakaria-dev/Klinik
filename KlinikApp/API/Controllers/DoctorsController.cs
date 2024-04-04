using BLC.Doctor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.PermissionRules;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private DoctorManager _manager;

        public DoctorsController(DoctorManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _manager.GetAllDoctors();

            return Ok(doctors);
        }

        [HttpGet]
        [Route("GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _manager.GetDoctorById(id);

            return Ok(doctor);
        }

        [HttpPut]
        [Route("UpdateDoctor")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> UpdateDoctor([FromBody] Shared.Models.Doctor doctor)
        {
            var updatedDoctor = await _manager.UpdateDoctor(doctor);

            return Ok(updatedDoctor);
        }

        [HttpPost]
        [Route("CreateDoctor")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> CreateDoctor([FromBody] Shared.Models.Doctor doctor)
        {
            var createdDoctor = await _manager.CreateDoctor(doctor);

            return Ok(createdDoctor);
        }

        [HttpDelete]
        [Route("DeleteDoctor")]
        [PermissionRule(Constants.adminRole)]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _manager.DeleteDoctor(id);

            return Ok(result);
        }
    }
}

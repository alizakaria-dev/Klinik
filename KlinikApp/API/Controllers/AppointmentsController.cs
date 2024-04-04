using BLC.Appointment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private AppointmentManager _manager;

        public AppointmentsController(AppointmentManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _manager.GetAllAppointments();

            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetAppointment")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _manager.GetAppointmentById(id);

            return Ok(appointment);
        }

        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment(Shared.Models.Appointment appointment)
        {
            var createdAppointment = await _manager.CreateAppointment(appointment);

            return Ok(createdAppointment);
        }

        [HttpPut]
        [Route("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment(Shared.Models.Appointment appointment)
        {
            var updatedAppointment = await _manager.UpdateAppointment(appointment);

            return Ok(updatedAppointment);
        }

        [HttpDelete]
        [Route("DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _manager.DeleteAppointment(id);

            return Ok(result);
        }
    }
}

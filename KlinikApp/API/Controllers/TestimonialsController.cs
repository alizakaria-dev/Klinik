using BLC.Testimonial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private TestimonialManager _manager;

        public TestimonialsController(TestimonialManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("GetAllTestimonials")]
        public async Task<IActionResult> GetAllTestimonials()
        {
            var testimonials = await _manager.GetAllTestimonials();

            return Ok(testimonials);
        }

        [HttpPost]
        [Route("CreateTestimonial")]
        public async Task<IActionResult> CreateTestimonial(Shared.Models.Testimonial testimonial)
        {
            var createdTestimonial = await _manager.CreateTestimonial(testimonial);

            return Ok(createdTestimonial);
        }

        [HttpPut]
        [Route("UpdateTestimonial")]
        public async Task<IActionResult> UpdateTestimonial(Shared.Models.Testimonial testimonial)
        {
            var updatedTestimonial = await _manager.UpdateTestimonial(testimonial);

            return Ok(updatedTestimonial);
        }

        [HttpDelete]
        [Route("DeleteTestimonial")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var result = await _manager.DeleteTestimonial(id);

            return Ok(result);
        }
    }
}

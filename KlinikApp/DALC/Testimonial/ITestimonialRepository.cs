namespace DALC.Testimonial
{
    public interface ITestimonialRepository
    {
        public Task<List<Shared.Models.Testimonial>> GetAllTestimonials();
        public Task<Shared.Models.Testimonial> CreateTestimonial(Shared.Models.Testimonial testimonial);
        public Task<Shared.Models.Testimonial> UpdateTestimonial(Shared.Models.Testimonial testimonial);
        public Task DeleteTestimonial(int id);
    }
}

using DALC.File;
using DALC.Testimonial;
using Shared.Models;
using System.Transactions;

namespace BLC.Testimonial
{
    public class TestimonialManager
    {
        private ITestimonialRepository _repository;
        private IFileRepository _fileRepository;

        public TestimonialManager(ITestimonialRepository repository, IFileRepository fileRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
        }

        public async Task<Result> GetAllTestimonials()
        {
            try
            {
                //USERTESTIMONIALIMAGE
                var testimonials = await _repository.GetAllTestimonials();

                if(testimonials == null || !testimonials.Any())
                {
                    return Result.Ok("No Testimonials were found", 400);
                }

                var relTable = "TESTIMONIALTABLE";
                var relField = "USERTESTIMONIALIMAGE";

                foreach (var testimonial in testimonials)
                {
                    var filesRetrieved = await _fileRepository.GetRelatedFiles(relField, relTable, testimonial.TESTIMONIALID);

                    var file = filesRetrieved.FirstOrDefault();

                    if(file != null)
                    {
                        file.URL = "http://localhost:5296/api/Files/" + file.FILEID.ToString() + "." + file.EXTENSION;
                    }

                    testimonial.File = file;
                }

                return Result.Ok(testimonials);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message,500);
            }
        }

        public async Task<Result> CreateTestimonial(Shared.Models.Testimonial testimonial)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdTestimonial = await _repository.CreateTestimonial(testimonial);

                    oScope.Complete();

                    return Result.Ok(createdTestimonial);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UpdateTestimonial(Shared.Models.Testimonial testimonial)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var updatedTestimonial = await _repository.UpdateTestimonial(testimonial);

                    oScope.Complete();

                    return Result.Ok(updatedTestimonial);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteTestimonial(int id)
        {
            using(TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteTestimonial(id);

                    oScope.Complete();

                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }
    }
}

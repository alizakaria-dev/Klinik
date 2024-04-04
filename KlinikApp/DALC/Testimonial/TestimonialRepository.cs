using DALC.Context;
using Dapper;

namespace DALC.Testimonial
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private DapperContext _context;
        public TestimonialRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<Shared.Models.Testimonial> CreateTestimonial(Shared.Models.Testimonial testimonial)
        {
            try
            {
                string procedure = "CREATETESTIMONIAL";

                var parameters = new DynamicParameters();

                using(var connection = _context.CreateConnection())
                {
                    parameters.Add("TESTIMONIALID", testimonial.TESTIMONIALID, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                    parameters.Add("USERNAME", testimonial.USERNAME, System.Data.DbType.String);
                    parameters.Add("DESCRIPTION", testimonial.DESCRIPTION, System.Data.DbType.String);
                    parameters.Add("PROFESSION", testimonial.PROFESSION, System.Data.DbType.String);

                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    int testimonialId = parameters.Get<int>("TESTIMONIALID");

                    var createdTestimonial = new Shared.Models.Testimonial
                    {
                        TESTIMONIALID = testimonialId,
                        USERNAME = testimonial.USERNAME,
                        DESCRIPTION = testimonial.DESCRIPTION,
                        PROFESSION = testimonial.PROFESSION,
                    };

                    return createdTestimonial;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteTestimonial(int id)
        {
            try
            {
                string procedure = "DELETETESTIMONIAL";

                var parameters = new DynamicParameters();

                using(var connection = _context.CreateConnection())
                {
                    parameters.Add("TESTIMONIALID",id,System.Data.DbType.Int32);
                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Shared.Models.Testimonial>> GetAllTestimonials()
        {
            string procedure = "GETALLTESTIMONIALS";

            using(var connection = _context.CreateConnection())
            {
                var testimonials = await connection.QueryAsync<Shared.Models.Testimonial>(procedure, commandType: System.Data.CommandType.StoredProcedure);

                var testimonialsList = testimonials.ToList();

                return testimonialsList;
            }
        }

        public async Task<Shared.Models.Testimonial> UpdateTestimonial(Shared.Models.Testimonial testimonial)
        {
            try
            {
                string procedure = "UPDATETESTIMONIAL";

                var parameters = new DynamicParameters();

                using (var connection = _context.CreateConnection())
                {
                    parameters.Add("TESTIMONIALID", testimonial.TESTIMONIALID, System.Data.DbType.Int32);
                    parameters.Add("USERNAME", testimonial.USERNAME, System.Data.DbType.String);
                    parameters.Add("DESCRIPTION", testimonial.DESCRIPTION, System.Data.DbType.String);
                    parameters.Add("PROFESSION", testimonial.PROFESSION, System.Data.DbType.String);

                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    return testimonial;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

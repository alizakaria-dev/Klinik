using DALC.Context;
using Dapper;

namespace DALC.Doctor
{
    public class DoctorRepository : IDoctorRepository
    {
        private DapperContext _context;

        public DoctorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.Doctor> CreateDoctor(Shared.Models.Doctor doctor)
        {
            try
            {
                var procedure = "CREATE_DOCTOR";

                var parameters = new DynamicParameters();

                parameters.Add("DOCTORID", doctor.DOCTORID, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                parameters.Add("NAME", doctor.NAME, System.Data.DbType.String);
                parameters.Add("DEPARTMENT", doctor.DEPARTMENT, System.Data.DbType.String);
                parameters.Add("FACEBOOKLINK", doctor.FACEBOOKLINK, System.Data.DbType.String);
                parameters.Add("TWITTERLINK", doctor.TWITTERLINK, System.Data.DbType.String);
                parameters.Add("INSTAGRAMLINK", doctor.INSTAGRAMLINK, System.Data.DbType.String);
                parameters.Add("DESCRIPTION", doctor.DESCRIPTION, System.Data.DbType.String);

                using(var connection  = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    int createdDoctorId = parameters.Get<int>("DOCTORID");
                    var createdDoctor = new Shared.Models.Doctor
                    {
                        DOCTORID = createdDoctorId,
                        NAME = doctor.NAME,
                        DEPARTMENT = doctor.DEPARTMENT,
                        FACEBOOKLINK = doctor.FACEBOOKLINK,
                        TWITTERLINK = doctor.TWITTERLINK,
                        INSTAGRAMLINK = doctor.INSTAGRAMLINK,
                        DESCRIPTION = doctor.DESCRIPTION
                    };

                    return createdDoctor;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteDoctor(int id)
        {
            var procedure = "DELETE_DOCTOR";

            var parameters = new DynamicParameters();

            parameters.Add("DOCTORID", id, System.Data.DbType.Int32);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(procedure, parameters,commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<List<Shared.Models.Doctor>> GetAllDoctors()
        {
            var procedure = "GET_ALL_DOCTORS";

            using(var connection = _context.CreateConnection())
            {
                var doctors = await connection.QueryAsync<Shared.Models.Doctor>(procedure, commandType: System.Data.CommandType.StoredProcedure);

                var doctorsList = doctors.ToList();

                return doctorsList;
            }

        }

        public async Task<Shared.Models.Doctor> GetDoctorById(int id)
        {
            var procedure = "GET_DOCTOR_BY_ID";

            var parameters = new DynamicParameters();

            parameters.Add("DOCTORID", id, System.Data.DbType.Int32);

            using(var connection = _context.CreateConnection())
            {
                var doctor = await connection.QueryFirstOrDefaultAsync<Shared.Models.Doctor>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return doctor;
            }
        }

        public async Task<Shared.Models.Doctor> UpdateDoctor(Shared.Models.Doctor doctor)
        {
            try
            {
                var procedure = "UPDATE_DOCTOR";

                var parameters = new DynamicParameters();

                parameters.Add("DOCTORID", doctor.DOCTORID, System.Data.DbType.Int32);
                parameters.Add("NAME", doctor.NAME, System.Data.DbType.String);
                parameters.Add("DEPARTMENT", doctor.DEPARTMENT, System.Data.DbType.String);
                parameters.Add("FACEBOOKLINK", doctor.FACEBOOKLINK, System.Data.DbType.String);
                parameters.Add("TWITTERLINK", doctor.TWITTERLINK, System.Data.DbType.String);
                parameters.Add("INSTAGRAMLINK", doctor.INSTAGRAMLINK, System.Data.DbType.String);
                parameters.Add("DESCRIPTION", doctor.DESCRIPTION, System.Data.DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    var updatedDoctor = await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    
                    return doctor;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

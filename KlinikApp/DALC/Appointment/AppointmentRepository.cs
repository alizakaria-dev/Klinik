using DALC.Context;
using Dapper;
using System.Data;

namespace DALC.Appointment
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private DapperContext _context;

        public AppointmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.Appointment> CreateAppointment(Shared.Models.Appointment appointment)
        {
            try
            {

                var procedure = "CREATEAPPOINTMENT";

                var parameters = new DynamicParameters();

                parameters.Add("APPOINTMENTID", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("NAME", appointment.NAME, DbType.String);
                parameters.Add("EMAIL", appointment.EMAIL, DbType.String);
                parameters.Add("DOCTOR", appointment.DOCTOR, DbType.String);
                parameters.Add("MOBILE", appointment.MOBILE, DbType.String);
                parameters.Add("DESCRIPTION", appointment.DESCRIPTION, DbType.String);
                parameters.Add("DATE", appointment.DATE, DbType.String);
                parameters.Add("TIME", appointment.TIME, DbType.Time);

                using(var connection = _context.CreateConnection())
                {
                    var result = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

                    var id = parameters.Get<int>("APPOINTMENTID");

                    var createdAppointment = new Shared.Models.Appointment
                    {
                        APPOINTMENTID = id,
                        NAME = appointment.NAME,
                        EMAIL = appointment.EMAIL,
                        DOCTOR = appointment.DOCTOR,
                        MOBILE = appointment.MOBILE,
                        DESCRIPTION = appointment.DESCRIPTION,
                        DATE = appointment.DATE,
                        TIME = appointment.TIME
                    };

                    return createdAppointment;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAppointment(int id)
        {
            try
            {
                var procedure = "DELETEAPPOINTMENT";

                var parameters = new DynamicParameters();

                parameters.Add("APPOINTMENTID", id, DbType.Int32);

                using(var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Shared.Models.Appointment>> GetAllAppointments()
        {
            try
            {
                var procedure = "GETALLAPPOINTMENTS";

                using(var connection = _context.CreateConnection())
                {
                    var appointments = await connection.QueryAsync<Shared.Models.Appointment>(procedure, commandType: CommandType.StoredProcedure);

                    return appointments.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.Appointment> GetAppointmenById(int id)
        {
            try
            {
                var procedure = "GETAPPOINTMENTBYID";

                var parameters = new DynamicParameters();

                parameters.Add("APPOINTMENTID", id, DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var appointment = await connection.QuerySingleOrDefaultAsync<Shared.Models.Appointment>(procedure,parameters, commandType: CommandType.StoredProcedure);

                    return appointment;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.Appointment> UpdateAppointment(Shared.Models.Appointment appointment)
        {
            try
            {
                var procedure = "UPDATEAPPOINTMENT";

                var parameters = new DynamicParameters();

                parameters.Add("APPOINTMENTID", appointment.APPOINTMENTID ,DbType.Int32);
                parameters.Add("NAME", appointment.NAME, DbType.String);
                parameters.Add("EMAIL", appointment.EMAIL, DbType.String);
                parameters.Add("DESCRIPTION", appointment.DESCRIPTION, DbType.String);
                parameters.Add("DOCTOR", appointment.DOCTOR, DbType.String);
                parameters.Add("MOBILE", appointment.MOBILE, DbType.String);
                parameters.Add("DATE", appointment.DATE, DbType.Date);
                parameters.Add("TIME", appointment.TIME, DbType.Time);

                using (var connection = _context.CreateConnection())
                {
                    var updatedAppointment = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

                    return appointment;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

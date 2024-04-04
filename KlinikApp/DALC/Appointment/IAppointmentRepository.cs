namespace DALC.Appointment
{
    public interface IAppointmentRepository
    {
        public Task<List<Shared.Models.Appointment>> GetAllAppointments();
        public Task<Shared.Models.Appointment> GetAppointmenById(int id);
        public Task<Shared.Models.Appointment> CreateAppointment(Shared.Models.Appointment appointment);
        public Task<Shared.Models.Appointment> UpdateAppointment(Shared.Models.Appointment appointment);
        public Task DeleteAppointment(int id);
    }
}

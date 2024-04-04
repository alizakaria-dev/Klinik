namespace DALC.Doctor
{
    public interface IDoctorRepository
    {
        public Task<List<Shared.Models.Doctor>> GetAllDoctors();
        public Task<Shared.Models.Doctor> GetDoctorById(int id);
        public Task<Shared.Models.Doctor> CreateDoctor(Shared.Models.Doctor doctor);
        public Task<Shared.Models.Doctor> UpdateDoctor(Shared.Models.Doctor doctor);
        public Task DeleteDoctor(int id);   
    }
}

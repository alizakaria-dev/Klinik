namespace DALC.Service
{
    public interface IServiceRepository
    {
        public Task<List<Shared.Models.Service>> GetAllServices();
        public Task<Shared.Models.Service> GetServiceById(int id);
        public Task<Shared.Models.Service> CreateService(Shared.Models.Service service);
        public Task<Shared.Models.Service> UpdateService(Shared.Models.Service service);
        public Task DeleteService(int id);
    }
}

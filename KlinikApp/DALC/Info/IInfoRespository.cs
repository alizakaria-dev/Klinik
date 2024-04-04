using Microsoft.AspNetCore.Http;

namespace DALC.Info
{
    public interface IInfoRespository
    {
        public Task<Shared.Models.Info> GetAllInfos();
        public Task<Shared.Models.Info> CreateInfo(Shared.Models.Info Info);
        public Task<Shared.Models.Info> UpdateInfo(Shared.Models.Info Info);
        public Task DeleteInfo(int id);
    }
}

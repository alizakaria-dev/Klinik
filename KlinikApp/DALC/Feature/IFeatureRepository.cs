namespace DALC.Feature
{
    public interface IFeatureRepository
    {
        public Task<Shared.Models.Feature> GetFeature();
        public Task<Shared.Models.Feature> UpdateFeature(Shared.Models.Feature feature);
        public Task<Shared.Models.Feature> CreateFeature(Shared.Models.Feature feature);
        public Task DeleteFeature(int id);
    }
}

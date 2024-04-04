namespace DALC.FeatureItem
{
    public interface IFeatureItemRepository
    {
        public Task<List<Shared.Models.FeatureItem>> GetAllFeatureItems();
        public Task<Shared.Models.FeatureItem> CreateFeatureItem(Shared.Models.FeatureItem featureItem);
        public Task<Shared.Models.FeatureItem> UpdateFeatureItem(Shared.Models.FeatureItem featureItem);
        public Task DeleteFeatureItem(int id);
    }
}

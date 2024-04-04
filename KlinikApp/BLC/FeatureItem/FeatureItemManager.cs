using DALC.FeatureItem;
using Shared.Models;
using System.Transactions;

namespace BLC.FeatureItem
{
    public class FeatureItemManager
    {
        private IFeatureItemRepository _repository;

        public FeatureItemManager(IFeatureItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> GetAllFeatureItems()
        {
            try
            {
                var featureItems = await _repository.GetAllFeatureItems();

                if(featureItems == null || !featureItems.Any())
                {
                    return Result.Ok("No Feature Items were found", 404);
                }

                return Result.Ok(featureItems);
            }
            catch (Exception ex)
            {

                return Result.Fail(ex.Message,500);
            }
        }

        public async Task<Result> CreateFeatureItem(Shared.Models.FeatureItem featureItem)
        {
            using(TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdFeatureItem = await _repository.CreateFeatureItem(featureItem);

                    oScope.Complete();

                    return Result.Ok(createdFeatureItem);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UpdateFeatureItem(Shared.Models.FeatureItem featureItem)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var updatedFeatureItem = await _repository.UpdateFeatureItem(featureItem);

                    oScope.Complete();

                    return Result.Ok(updatedFeatureItem);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteFeatureItem(int id)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteFeatureItem(id);

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

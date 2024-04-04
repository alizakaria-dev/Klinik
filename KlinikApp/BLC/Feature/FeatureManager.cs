using DALC.Feature;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLC.Feature
{
    public class FeatureManager
    {
        private IFeatureRepository _repository;

        public FeatureManager(IFeatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> GetFeature()
        {
            try
            {
                var feature = await _repository.GetFeature();

                if (feature == null)
                {
                    return Result.Ok("No Feature was found", 404);
                }

                return Result.Ok(feature);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> CreateFeature(Shared.Models.Feature feature)
        {
            try
            {
                var createdFeature = await _repository.CreateFeature(feature);

                return Result.Ok(createdFeature);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> UpdateFeature(Shared.Models.Feature feature)
        {
            try
            {
                var updatedFeature = await _repository.UpdateFeature(feature);

                return Result.Ok(updatedFeature);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> DeleteFeature(int id)
        {
            try
            {
                await _repository.DeleteFeature(id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }
    }
}

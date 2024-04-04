using DALC.Context;
using Dapper;
using Shared.Models;
using System.Data;

namespace DALC.Feature
{
    public class FeatureRepository : IFeatureRepository
    {
        private DapperContext _context;

        public FeatureRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.Feature> CreateFeature(Shared.Models.Feature feature)
        {
            try
            {
                var procedure = "CREATE_FEATURE";

                var parameters = new DynamicParameters();

                parameters.Add("FEATUREID", feature.FEATUREID, System.Data.DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("TEXT", feature.TEXT, System.Data.DbType.String);
                parameters.Add("TITLE", feature.TITLE, System.Data.DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                    int createdFeatureId = parameters.Get<int>("FEATUREID");
                    var createdFeature = new Shared.Models.Feature
                    {
                        FEATUREID = createdFeatureId,
                        TEXT = feature.TEXT,
                        TITLE = feature.TITLE
                    };

                    return createdFeature;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task DeleteFeature(int id)
        {
            try
            {
                var procedure = "DELETE_FEATURE";

                var parameters = new DynamicParameters();

                parameters.Add("FEATUREID", id, DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.Feature> GetFeature()
        {
            try
            {
                var featureProcedure = "GET_FEATURE";
                var featureItemProcedure = "GET_FEATUREITEMS";

                var parameters = new DynamicParameters();

                var procedure = featureProcedure + featureItemProcedure;

                using (var connection = _context.CreateConnection())
                {
                    
                    var feature = await connection.QueryFirstOrDefaultAsync<Shared.Models.Feature>(featureProcedure, commandType: CommandType.StoredProcedure);

                    parameters.Add("FEATUREID", feature.FEATUREID, DbType.Int32);

                    var featureItems = await connection.QueryAsync<Shared.Models.FeatureItem>(featureItemProcedure,parameters, commandType: CommandType.StoredProcedure);

                    var featureItemsList = featureItems.ToList();

                    feature.FEATUREITEMS = featureItemsList;

                    return feature;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.Feature> UpdateFeature(Shared.Models.Feature feature)
        {
            try
            {
                var procedure = "UPDATE_FEATURE";

                var parameters = new DynamicParameters();

                parameters.Add("FEATUREID", feature.FEATUREID, System.Data.DbType.Int32);
                parameters.Add("TEXT", feature.TEXT, System.Data.DbType.String);
                parameters.Add("TITLE", feature.TITLE, System.Data.DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);

                    return feature;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

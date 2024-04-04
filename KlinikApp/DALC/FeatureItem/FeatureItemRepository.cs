using DALC.Context;
using Dapper;

namespace DALC.FeatureItem
{
    public class FeatureItemRepository : IFeatureItemRepository
    {
        private DapperContext _context;

        public FeatureItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.FeatureItem> CreateFeatureItem(Shared.Models.FeatureItem featureItem)
        {
            try
            {
                var procedure = "CREATE_FEATUREITEM";

                var parameters = new DynamicParameters();

                parameters.Add("FEATUREITEMID", featureItem.FEATUREITEMID, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                parameters.Add("FEATUREID", featureItem.FEATUREID, System.Data.DbType.Int32);
                parameters.Add("ICON", featureItem.ICON, System.Data.DbType.String);
                parameters.Add("TEXTONE", featureItem.TEXTONE, System.Data.DbType.String);
                parameters.Add("TEXTTWO", featureItem.TEXTTWO, System.Data.DbType.String);

                using(var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters,commandType : System.Data.CommandType.StoredProcedure);
                    int featureItemId = parameters.Get<int>("FEATUREITEMID");
                    var createdFeatureItem = new Shared.Models.FeatureItem
                    {
                        FEATUREID = featureItem.FEATUREID,
                        FEATUREITEMID = featureItemId,
                        ICON = featureItem.ICON,
                        TEXTONE = featureItem.TEXTONE,
                        TEXTTWO = featureItem.TEXTTWO,
                    };

                    return createdFeatureItem;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteFeatureItem(int id)
        {
            try
            {
                var procedure = "DELETE_FEATUREITEM";

                var parameters = new DynamicParameters();

                parameters.Add("FEATUREITEMID", id, System.Data.DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Shared.Models.FeatureItem>> GetAllFeatureItems()
        {
            try
            {
                var procedure = "GET_FEATUREITEMS";

                using (var connection = _context.CreateConnection())
                {
                    var featureItems = await connection.QueryAsync<Shared.Models.FeatureItem>(procedure, commandType: System.Data.CommandType.StoredProcedure);
                    var featureItemsList = featureItems.ToList();
                    return featureItemsList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.FeatureItem> UpdateFeatureItem(Shared.Models.FeatureItem featureItem)
        {
            try
            {
                var procedure = "UPDATE_FEATUREITEM";

                var parameters = new DynamicParameters();

                parameters.Add("FEATUREITEMID", featureItem.FEATUREITEMID, System.Data.DbType.Int32);
                parameters.Add("FEATUREID", featureItem.FEATUREID, System.Data.DbType.Int32);
                parameters.Add("ICON", featureItem.ICON, System.Data.DbType.String);
                parameters.Add("TEXTONE", featureItem.TEXTONE, System.Data.DbType.String);
                parameters.Add("TEXTTWO", featureItem.TEXTTWO, System.Data.DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return featureItem;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

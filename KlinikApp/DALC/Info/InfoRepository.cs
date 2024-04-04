using DALC.Context;
using Dapper;
using Microsoft.AspNetCore.Http;
using Shared.Models;
using System.Data;

namespace DALC.Info
{
    public class InfoRepository : IInfoRespository
    {
        private DapperContext _context;

        public InfoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.Info> CreateInfo(Shared.Models.Info info)
        {
            try
            {
                var procedure = "ADDINFO";
                var parameters = new DynamicParameters();

                using (var connection = _context.CreateConnection())
                {

                    parameters.Add("STAFF", info.STAFF, DbType.Int32);
                    parameters.Add("DOCTORS", info.DOCTORS, DbType.Int32);
                    parameters.Add("PATIENTS", info.PATIENTS, DbType.Int32);
                    parameters.Add("INFOID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                    var infoId = parameters.Get<int>("INFOID");
                    var addedInfo = new Shared.Models.Info
                    {
                        INFOID = infoId,
                        STAFF = info.STAFF,
                        DOCTORS = info.DOCTORS,
                        PATIENTS = info.PATIENTS,
                    };
                    return addedInfo;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteInfo(int id)
        {
            try
            {
                var procedure = "DELETEINFO";

                var parameters = new DynamicParameters();

                parameters.Add("INFOID", id, DbType.Int32);

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

        public async Task<Shared.Models.Info> GetAllInfos()
        {
            try
            {
                var procedure = "GETINFO";

                using(var connection = _context.CreateConnection())
                {
                    var info = await connection.QueryFirstOrDefaultAsync<Shared.Models.Info>(procedure, commandType: CommandType.StoredProcedure);

                    return info;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.Info> UpdateInfo(Shared.Models.Info info)
        {
            try
            {
                var procedure = "UPDATEINFO";
                var parameters = new DynamicParameters();
                parameters.Add("STAFF", info.STAFF, DbType.Int32);
                parameters.Add("DOCTORS", info.DOCTORS, DbType.Int32);
                parameters.Add("PATIENTS", info.PATIENTS, DbType.Int32);
                parameters.Add("INFOID",info.INFOID,DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var updatedInfo = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                    
                    return info;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

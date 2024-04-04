using DALC.Context;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALC.Service
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DapperContext _context;
        public ServiceRepository(DapperContext context)
        {
            _context= context;
        }
        public async Task<Shared.Models.Service> CreateService(Shared.Models.Service service)
        {
            try
            {
                string procedure = "CREATE_SERVICE";

                var parameters = new DynamicParameters();

                parameters.Add("SERVICEID", service.SERVICEID, System.Data.DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("ICON", service.ICON, System.Data.DbType.String);
                parameters.Add("TITLE", service.TITLE, System.Data.DbType.String);
                parameters.Add("TEXT", service.TEXT, System.Data.DbType.String);
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    int createdServiceId = parameters.Get<int>("SERVICEID");
                    var createdService = new Shared.Models.Service
                    {
                        SERVICEID = createdServiceId,
                        TITLE = service.TITLE,
                        TEXT = service.TEXT,
                        ICON = service.ICON,
                    };

                    return createdService;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteService(int id)
        {
            try
            {
                string procedure = "DELETE_SERVICE";
                var parameters = new DynamicParameters();
                parameters.Add("SERVICEID", id, System.Data.DbType.Int32);
                using (var connection = _context.CreateConnection())
                {
                    var x = await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Shared.Models.Service>> GetAllServices()
        {
            try
            {
                string procedure = "GET_ALL_SERVICES";
                using (var connection = _context.CreateConnection())
                {
                    var services = await connection.QueryAsync<Shared.Models.Service>(procedure, commandType: System.Data.CommandType.StoredProcedure);
                    var returnedServices = services.ToList();

                    return returnedServices;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.Service> GetServiceById(int id)
        {
            try
            {
                string procedure = "GET_SERVICE_BY_ID";
                var parameter = new DynamicParameters();
                parameter.Add("SERVICEID", id, System.Data.DbType.Int32);
                using (var connection = _context.CreateConnection())
                {
                    var service = await connection.QuerySingleOrDefaultAsync<Shared.Models.Service>(procedure, parameter, commandType: System.Data.CommandType.StoredProcedure);
                    return service;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.Service> UpdateService(Shared.Models.Service service)
        {
            try
            {
                string procedure = "UPDATE_SERVICE";

                var parameters = new DynamicParameters();

                parameters.Add("SERVICEID", service.SERVICEID, System.Data.DbType.Int32);
                parameters.Add("ICON", service.ICON, System.Data.DbType.String);
                parameters.Add("TITLE", service.TITLE, System.Data.DbType.String);
                parameters.Add("TEXT", service.TEXT, System.Data.DbType.String);
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }

                return service;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

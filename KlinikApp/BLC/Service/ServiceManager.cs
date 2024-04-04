using DALC.Service;
using Shared.Models;
using System.Data;
using System.Transactions;

namespace BLC.Service
{
    public class ServiceManager
    {
        private IServiceRepository _repository;
        public ServiceManager(IServiceRepository respository)
        {
            _repository= respository;
        }

        public async Task<Result> GetAllServices()
        {
            try
            {
                var services = await _repository.GetAllServices();

                if(services == null || !services.Any())
                {
                    return Result.Ok("No Services were found", 404);
                }

                return Result.Ok(services);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> GetServiceById(int id)
        {
            try
            {
                var service = await _repository.GetServiceById(id);

                if(service == null)
                {
                    return Result.Ok("The service was not found", 404);
                }

                return Result.Ok(service);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> CreateService(Shared.Models.Service service)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdService = await _repository.CreateService(service);

                    oScope.Complete();

                    return Result.Ok(createdService);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UpdateService(Shared.Models.Service service)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var updatedService = await _repository.UpdateService(service);

                    oScope.Complete();

                    return Result.Ok(updatedService);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteService(int id)
        {
            using(TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteService(id);

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

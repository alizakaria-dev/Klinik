using DALC.File;
using DALC.Info;
using Microsoft.AspNetCore.Http;
using Shared.Models;
using System.Transactions;

namespace BLC.Info
{
    public class InfoManager
    {
        private IInfoRespository _repository;
        private IFileRepository _fileRepository;
        private IHttpContextAccessor _contextAccessor;

        public InfoManager(IInfoRespository repository, IFileRepository fileRepository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _fileRepository = fileRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result> CreateInfo(Shared.Models.Info info)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdDoctor = await _repository.CreateInfo(info);

                    oScope.Complete();

                    return Result.Ok(createdDoctor);
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> GetInfo()
        {
            try
            {
                var info = await _repository.GetAllInfos();

                var uploadedFiles = await _fileRepository.GetRelatedFiles("INFO_IMAGES", "INFOTABLE", 0);

                foreach (var file in uploadedFiles)
                {
                    file.URL = "http://localhost:5296/api/Files/" + file.FILEID.ToString() + "." + file.EXTENSION;
                }

                info.FILES = uploadedFiles;

                if (info == null)
                {
                    return Result.Ok("No info were found", 404);
                }

                return Result.Ok(info);
            }
            catch (Exception ex)
            {

                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> UpdateInfo(Shared.Models.Info info)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdDoctor = await _repository.UpdateInfo(info);

                    oScope.Complete();

                    return Result.Ok(createdDoctor);
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteInfo(int id)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteInfo(id);

                    oScope.Complete();

                    return Result.Ok();
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UploadInfoFile(IFormFileCollection files)
        {
            var uploadedFiles = await _fileRepository.UploadFile(files, null, "INFOTABLE", "INFO_IMAGES");

            return Result.Ok(uploadedFiles);
        }
    }
}

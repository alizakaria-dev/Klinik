using DALC.File;
using Microsoft.AspNetCore.Http;
using Shared.Models;
using System.IO;
using System.Reflection;
using System.Transactions;

namespace BLC.File
{
    public class FileManager
    {
        private IFileRepository _repository;
        private IHttpContextAccessor _accessor;

        public FileManager(IFileRepository repository, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<Result> AddFile(Shared.Models.File file)
        {
            using(TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdFile = await _repository.AddFile(file);

                    oScope.Complete();

                    return Result.Ok(createdFile);
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteFile(int id)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteFile(id);

                    var folderPath = @$"C:\PracticeProjects\CssTemplates\Klinik\App\KlinikSolution\KLINIK\API\Files";

                    string[] files = Directory.GetFiles(folderPath);

                    string? file = files?.FirstOrDefault(path => Path.GetFileNameWithoutExtension(path) == id.ToString());

                    if (file != null)
                    {
                        System.IO.File.Delete(file);
                    }

                    oScope.Complete();

                    return Result.Ok();
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> GetRelatedFiles(string relField, string relTable, int relKey)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdFile = await _repository.GetRelatedFiles(relField,relTable,relKey);

                    oScope.Complete();

                    return Result.Ok(createdFile);
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UploadFile(IFormFileCollection files, int? relKey, string relTable, string relField)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdFile = await _repository.UploadFile(files,relKey,relTable,relField);

                    oScope.Complete();

                    return Result.Ok(createdFile);
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }
    }
}

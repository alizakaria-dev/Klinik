using DALC.Context;
using Dapper;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace DALC.File
{
    public class FileRepository : IFileRepository
    {
        private DapperContext _context;

        public FileRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Shared.Models.File> AddFile(Shared.Models.File file)
        {
            try
            {
                var procedure = "ADD_FILE";

                var parameters = new DynamicParameters();

                parameters.Add("FILEID", file.FILEID, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                parameters.Add("REL_KEY", file.REL_KEY, System.Data.DbType.Int32);
                parameters.Add("SIZE", file.SIZE, System.Data.DbType.Int32);
                parameters.Add("REL_TABLE", file.REL_TABLE, System.Data.DbType.String);
                parameters.Add("REL_FIELD", file.REL_FIELD, System.Data.DbType.String);
                parameters.Add("EXTENSION", file.EXTENSION, System.Data.DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    var fileId = parameters.Get<int>("FILEID");
                    var createdFile = new Shared.Models.File
                    {
                        FILEID = fileId,
                        REL_KEY = file.REL_KEY,
                        SIZE = file.SIZE,
                        REL_TABLE = file.REL_TABLE,
                        REL_FIELD = file.REL_FIELD,
                        EXTENSION = file.EXTENSION
                    };

                    return createdFile;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteFile(int id)
        {
            try
            {
                var procedure = "DELETE_FILE";

                var parameters = new DynamicParameters();

                parameters.Add("FILEID", id, System.Data.DbType.Int32);

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

        public async Task<List<Shared.Models.File>> GetRelatedFiles(string relField, string relTable, int relKey)
        {
            try
            {
                var procedure = "GET_REL_FILES";

                var parameters = new DynamicParameters();

                parameters.Add("REL_FIELD", relField, System.Data.DbType.String);
                parameters.Add("REL_TABLE", relTable, System.Data.DbType.String);
                parameters.Add("REL_KEY", relKey, System.Data.DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var relatedFiles = await connection.QueryAsync<Shared.Models.File>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    var relatedFilesList = relatedFiles.ToList();
                    return relatedFilesList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Shared.Models.File> ExtractFileFromRequest(HttpContext httpContext, int id)
        {
            var relTable = httpContext.Request.Query["RelTable"].FirstOrDefault();
            var relField = httpContext.Request.Query["RelField"].FirstOrDefault();
            var relKey = id;
            var files = httpContext.Request.Form.Files;
            string newFileName = "";

            if (files.Any())
            {
                var queryFile = files.FirstOrDefault();
                var fileName = queryFile.FileName;

                var splitFileName = fileName.Split(".");

                var fileExtension = splitFileName[1];

                var uploadedFile = new Shared.Models.File();

                uploadedFile.SIZE = queryFile.Length / 1024;

                uploadedFile.REL_TABLE = relTable;

                uploadedFile.REL_KEY = relKey;

                uploadedFile.REL_FIELD = relField;

                uploadedFile.EXTENSION = fileExtension;

                var insertedFile = await AddFile(uploadedFile);

                newFileName = insertedFile.FILEID.ToString();

                var fullFilePath = String.Format("{0}{1}.{2}", @"C:\PracticeProjects\CssTemplates\Klinik\App\KlinikSolution\KLINIK\API\Files\", newFileName, fileExtension);

                using (var stream = System.IO.File.Create(fullFilePath))
                {
                    await queryFile.CopyToAsync(stream);
                }

                insertedFile.URL = "http://localhost:5296/api/Files/" + insertedFile.FILEID.ToString() + "." + insertedFile.EXTENSION;

                return insertedFile;
            }
            else
            {
                return null;
            }
        }

        public async Task<Shared.Models.File> UploadFile(IFormFileCollection files, int? relKey, string relTable, string relField)
        {
            string newFileName = "";

            var uploadedFile = new Shared.Models.File();


            if (files.Any())
            {
                foreach (var file in files)
                {
                    var fileName = file.FileName;

                    var splitFileName = fileName.Split(".");

                    var fileExtension = splitFileName[1];

                    uploadedFile.SIZE = file.Length / 1024;

                    uploadedFile.REL_TABLE = relTable;

                    if (relKey.HasValue)
                    {
                        uploadedFile.REL_KEY = relKey.Value;
                    }

                    uploadedFile.REL_FIELD = relField;

                    uploadedFile.EXTENSION = fileExtension;

                    var insertedFile = await AddFile(uploadedFile);

                    newFileName = insertedFile.FILEID.ToString();

                    var fullFilePath = String.Format("{0}{1}.{2}", @"C:\PracticeProjects\CssTemplates\Klinik\App\KlinikSolution\KLINIK\API\Files\", newFileName, fileExtension);

                    using (var stream = System.IO.File.Create(fullFilePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    insertedFile.URL = "http://localhost:5296/api/Files/" + insertedFile.FILEID.ToString() + "." + insertedFile.EXTENSION;

                }
                return uploadedFile;
            }
            else
            {
                return null;
            }
        }
    }
}

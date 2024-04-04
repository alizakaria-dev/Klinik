using Microsoft.AspNetCore.Http;

namespace DALC.File
{
    public interface IFileRepository
    {
        public Task<List<Shared.Models.File>> GetRelatedFiles(string relField, string relTable, int relKey);

        public Task<Shared.Models.File> AddFile(Shared.Models.File file);

        public Task DeleteFile(int id);

        public Task<Shared.Models.File> ExtractFileFromRequest(HttpContext httpContext, int id);

        public Task<Shared.Models.File> UploadFile(IFormFileCollection files, int? relKey, string relTable, string relField);
    }
}

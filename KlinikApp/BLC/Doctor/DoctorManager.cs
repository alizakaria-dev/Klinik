using DALC.Doctor;
using DALC.File;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using Shared.Models;
using System.Numerics;
using System.Transactions;

namespace BLC.Doctor
{
    public class DoctorManager
    {
        private IDoctorRepository _repository;
        private IFileRepository _fileRepository;
        private IHttpContextAccessor _contextAccessor;

        public DoctorManager(IDoctorRepository repository, IFileRepository fileRepository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _fileRepository = fileRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result> GetAllDoctors()
        {
            try
            {
                var doctors = await _repository.GetAllDoctors();

                if (doctors == null || !doctors.Any())
                {
                    return Result.Ok("No Doctors were found", 404);
                }

                var relTable = "DOCTORTABLE";
                var relField = "DoctorImage";

                foreach (var doctor in doctors)
                {
                    var filesRetrieved = await _fileRepository.GetRelatedFiles(relField,relTable,doctor.DOCTORID);
                    //if (filesRetrieved != null)
                    //{
                    //    filesRetrieved.URL = "http://localhost:5296/api/Files/" + filesRetrieved..ToString() + "." + filesRetrieved.Extension;
                    //}
                    filesRetrieved = filesRetrieved.Select(x =>
                    {
                        x.URL = "http://localhost:5296/api/Files/" + x.FILEID.ToString() + "." + x.EXTENSION;
                        return x;
                    }).ToList();

                    doctor.Files = filesRetrieved;
                }

                

                return Result.Ok(doctors);
            }
            catch (Exception ex)
            {

                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _repository.GetDoctorById(id);

                var relTable = "DOCTORTABLE";
                var relField = "DoctorImage";

                if (doctor == null)
                {
                    return Result.Ok("The Doctor was not found", 404);
                }

                var relatedFiles = await _fileRepository.GetRelatedFiles(relField, relTable, doctor.DOCTORID);

                if (relatedFiles != null && relatedFiles.Any())
                {
                    if (doctor.Files == null)
                    {
                        doctor.Files = new List<Shared.Models.File>();
                    }
                    foreach (var file in relatedFiles)
                    {
                        file.URL = "http://localhost:5296/api/Files/" + file.FILEID.ToString() + "." + file.EXTENSION;
                        doctor.Files.Add(file);
                    }
                }

                return Result.Ok(doctor);
            }
            catch (Exception ex)
            {

                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> CreateDoctor(Shared.Models.Doctor doctor)
        {
            using(TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdDoctor = await _repository.CreateDoctor(doctor);

                    oScope.Complete();

                    return Result.Ok(createdDoctor);
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UpdateDoctor(Shared.Models.Doctor doctor)
        {
            using(TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var updateDoctor = await _repository.UpdateDoctor(doctor);

                    oScope.Complete();

                    return Result.Ok(updateDoctor);
                }
                catch (Exception ex)
                {

                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> DeleteDoctor(int id)
        {
            using(TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var relTable = "DOCTORTABLE";
                    var relField = "DoctorImage";

                    await _repository.DeleteDoctor(id);

                    var filesRetrieved = await _fileRepository.GetRelatedFiles(relField, relTable, id);

                    if (filesRetrieved.Any())
                    {
                        foreach (var file in filesRetrieved)
                        {
                            await _fileRepository.DeleteFile(file.FILEID);

                            if (System.IO.File.Exists(@$"C:\PracticeProjects\CssTemplates\Klinik\App\KlinikSolution\KLINIK\API\Files\{file.FILEID}.{file.EXTENSION}"))
                            {
                                System.IO.File.Delete(@$"C:\PracticeProjects\CssTemplates\Klinik\App\KlinikSolution\KLINIK\API\Files\{file.FILEID}.{file.EXTENSION}");
                            }
                        }
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
    }
}

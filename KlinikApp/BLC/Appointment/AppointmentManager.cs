using DALC.Appointment;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Shared.Extensions;
using Shared.Models;
using System.Globalization;
using System.Transactions;

namespace BLC.Appointment
{
    public class AppointmentManager
    {
        private IAppointmentRepository _repository;

        public AppointmentManager(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> GetAllAppointments()
        {
            try
            {
                var appointments = await _repository.GetAllAppointments();

                foreach (var appointment in appointments)
                {
                   appointment.DATE = appointment.DATE.StringToDateFormat();
                }

                return Result.Ok(appointments);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _repository.GetAppointmenById(id);

                appointment.DATE = appointment.DATE.StringToDateFormat();

                return Result.Ok(appointment);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message, 500);
            }
        }

        public async Task<Result> DeleteAppointment(int id)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _repository.DeleteAppointment(id);

                    oScope.Complete();

                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> CreateAppointment(Shared.Models.Appointment appointment)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    bool isStringDateTimeFormat = appointment.DATE.IsStringDateTimeFormat();

                    if(!isStringDateTimeFormat)
                    {
                        return Result.Fail("Please send a valid date format", 400);
                    }

                    appointment.DATE = appointment.DATE.StringToDateTimeFormat();

                    var createdAppointment = await _repository.CreateAppointment(appointment);

                    oScope.Complete();

                    return Result.Ok(createdAppointment);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }

        public async Task<Result> UpdateAppointment(Shared.Models.Appointment appointment)
        {
            using (TransactionScope oScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    bool isStringDateTimeFormat = appointment.DATE.IsStringDateTimeFormat();

                    if (!isStringDateTimeFormat)
                    {
                        return Result.Fail("Please send a valid date format", 400);
                    }

                    appointment.DATE = appointment.DATE.StringToDateTimeFormat();

                    var updatedAppointment = await _repository.UpdateAppointment(appointment);

                    oScope.Complete();

                    return Result.Ok(updatedAppointment);
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message, 500);
                }
            }
        }
    }
}

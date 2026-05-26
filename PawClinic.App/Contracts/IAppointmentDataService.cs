using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Contracts;

public interface IAppointmentDataService
{
    Task<List<AppointmentListViewModel>> GetUpcomingAppointments();
    Task<List<AppointmentListViewModel>> GetAppointmentHistory(Guid petId);
    Task<AppointmentDetailViewModel?> GetAppointmentById(Guid id);
    Task<ApiResponse<Guid>> ScheduleAppointment(Guid petId, Guid vetId, DateTime scheduledDateTime, string reasonForVisit);
    Task<ApiResponse<bool>> CancelAppointment(Guid id, string? reason);
    Task<ApiResponse<bool>> CompleteAppointment(Guid id, string notes);
}

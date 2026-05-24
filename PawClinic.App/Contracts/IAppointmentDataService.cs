using PawClinic.App.ViewModels;

namespace PawClinic.App.Contracts;

public interface IAppointmentDataService
{
    Task<List<AppointmentListViewModel>> GetUpcomingAppointments();
    Task<List<AppointmentListViewModel>> GetAppointmentHistory(Guid petId);
}

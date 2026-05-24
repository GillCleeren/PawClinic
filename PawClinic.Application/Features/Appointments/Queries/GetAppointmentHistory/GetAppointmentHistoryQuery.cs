using MediatR;
using PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments;

namespace PawClinic.Application.Features.Appointments.Queries.GetAppointmentHistory
{
    public class GetAppointmentHistoryQuery : IRequest<List<AppointmentListVm>>
    {
        public Guid PetId { get; set; }
    }
}

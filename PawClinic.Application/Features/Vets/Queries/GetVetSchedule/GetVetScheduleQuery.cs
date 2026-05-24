using MediatR;
using PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments;

namespace PawClinic.Application.Features.Vets.Queries.GetVetSchedule
{
    public class GetVetScheduleQuery : IRequest<List<AppointmentListVm>>
    {
        public Guid VetId { get; set; }
        public DateOnly Date { get; set; }
    }
}

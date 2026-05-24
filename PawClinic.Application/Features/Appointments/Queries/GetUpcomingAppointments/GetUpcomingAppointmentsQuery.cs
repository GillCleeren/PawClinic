using MediatR;

namespace PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments
{
    public class GetUpcomingAppointmentsQuery : IRequest<List<AppointmentListVm>>
    {
        public Guid? VetId { get; set; }
        public Guid? PetId { get; set; }
    }
}

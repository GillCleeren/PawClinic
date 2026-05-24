using MediatR;

namespace PawClinic.Application.Features.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<AppointmentDetailVm>
    {
        public Guid AppointmentId { get; set; }
    }
}

using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Exceptions;
using PawClinic.Domain.Entities;
using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Unit> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
                throw new NotFoundException(nameof(Appointment), request.AppointmentId);

            if (appointment.Status == AppointmentStatus.Completed)
                throw new BadRequestException("Cannot cancel an appointment that is already completed.");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new BadRequestException("This appointment is already cancelled.");

            appointment.Status = AppointmentStatus.Cancelled;
            if (!string.IsNullOrWhiteSpace(request.Reason))
                appointment.Notes = request.Reason;

            await _appointmentRepository.UpdateAsync(appointment);
            return Unit.Value;
        }
    }
}

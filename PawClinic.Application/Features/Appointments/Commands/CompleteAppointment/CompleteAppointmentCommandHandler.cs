using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Exceptions;
using PawClinic.Domain.Entities;
using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Appointments.Commands.CompleteAppointment
{
    public class CompleteAppointmentCommandHandler : IRequestHandler<CompleteAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CompleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Unit> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
                throw new NotFoundException(nameof(Appointment), request.AppointmentId);

            if (appointment.Status != AppointmentStatus.Scheduled)
                throw new BadRequestException($"Cannot complete an appointment with status '{appointment.Status}'.");

            if (appointment.ScheduledDateTime > DateTime.UtcNow)
                throw new BadRequestException("Cannot complete an appointment that hasn't happened yet.");

            appointment.Status = AppointmentStatus.Completed;
            appointment.Notes = request.Notes;

            await _appointmentRepository.UpdateAsync(appointment);
            return Unit.Value;
        }
    }
}

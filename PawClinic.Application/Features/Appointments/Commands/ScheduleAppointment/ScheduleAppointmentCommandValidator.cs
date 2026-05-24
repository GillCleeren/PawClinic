using FluentValidation;
using PawClinic.Application.Contracts.Persistence;

namespace PawClinic.Application.Features.Appointments.Commands.ScheduleAppointment
{
    public class ScheduleAppointmentCommandValidator : AbstractValidator<ScheduleAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ScheduleAppointmentCommandValidator(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;

            RuleFor(p => p.ScheduledDateTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Appointment must be scheduled in the future.");

            RuleFor(p => p.ReasonForVisit)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p)
                .MustAsync(VetNotDoubleBooked).WithMessage("This vet already has an appointment at that time.")
                .MustAsync(PetNotDoubleBooked).WithMessage("This pet already has an appointment at that time.");
        }

        private async Task<bool> VetNotDoubleBooked(ScheduleAppointmentCommand cmd, CancellationToken token)
            => !await _appointmentRepository.IsVetDoubleBookedAsync(cmd.VetId, cmd.ScheduledDateTime);

        private async Task<bool> PetNotDoubleBooked(ScheduleAppointmentCommand cmd, CancellationToken token)
            => !await _appointmentRepository.IsPetDoubleBookedAsync(cmd.PetId, cmd.ScheduledDateTime);
    }
}

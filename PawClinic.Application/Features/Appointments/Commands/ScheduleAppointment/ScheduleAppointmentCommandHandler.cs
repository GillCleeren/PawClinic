using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;
using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Appointments.Commands.ScheduleAppointment
{
    public class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommand, Guid>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public ScheduleAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(ScheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new ScheduleAppointmentCommandValidator(_appointmentRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var appointment = _mapper.Map<Appointment>(request);
            appointment.Status = AppointmentStatus.Scheduled;

            appointment = await _appointmentRepository.AddAsync(appointment);
            return appointment.AppointmentId;
        }
    }
}

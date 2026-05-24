using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;

namespace PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments
{
    public class GetUpcomingAppointmentsQueryHandler : IRequestHandler<GetUpcomingAppointmentsQuery, List<AppointmentListVm>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetUpcomingAppointmentsQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<List<AppointmentListVm>> Handle(GetUpcomingAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetUpcomingAsync(request.VetId, request.PetId);
            return _mapper.Map<List<AppointmentListVm>>(appointments);
        }
    }
}

using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments;

namespace PawClinic.Application.Features.Appointments.Queries.GetAppointmentHistory
{
    public class GetAppointmentHistoryQueryHandler : IRequestHandler<GetAppointmentHistoryQuery, List<AppointmentListVm>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetAppointmentHistoryQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<List<AppointmentListVm>> Handle(GetAppointmentHistoryQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetHistoryAsync(request.PetId);
            return _mapper.Map<List<AppointmentListVm>>(appointments);
        }
    }
}

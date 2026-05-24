using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments;

namespace PawClinic.Application.Features.Vets.Queries.GetVetSchedule
{
    public class GetVetScheduleQueryHandler : IRequestHandler<GetVetScheduleQuery, List<AppointmentListVm>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetVetScheduleQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<List<AppointmentListVm>> Handle(GetVetScheduleQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetVetScheduleAsync(request.VetId, request.Date);
            return _mapper.Map<List<AppointmentListVm>>(appointments);
        }
    }
}

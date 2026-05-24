using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Exceptions;
using PawClinic.Domain.Entities;

namespace PawClinic.Application.Features.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDetailVm>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAsyncRepository<Pet> _petRepository;
        private readonly IAsyncRepository<Vet> _vetRepository;
        private readonly IMapper _mapper;

        public GetAppointmentByIdQueryHandler(
            IAppointmentRepository appointmentRepository,
            IAsyncRepository<Pet> petRepository,
            IAsyncRepository<Vet> vetRepository,
            IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _petRepository = petRepository;
            _vetRepository = vetRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentDetailVm> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
                throw new NotFoundException(nameof(Appointment), request.AppointmentId);

            var vm = _mapper.Map<AppointmentDetailVm>(appointment);

            var pet = await _petRepository.GetByIdAsync(appointment.PetId);
            var vet = await _vetRepository.GetByIdAsync(appointment.VetId);

            vm.Pet = _mapper.Map<PetDto>(pet);
            vm.Vet = _mapper.Map<VetDto>(vet);

            return vm;
        }
    }
}

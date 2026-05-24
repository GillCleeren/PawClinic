using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Exceptions;
using PawClinic.Domain.Entities;

namespace PawClinic.Application.Features.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, PetDetailVm>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public GetPetByIdQueryHandler(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<PetDetailVm> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetByIdAsync(request.PetId);
            if (pet == null)
                throw new NotFoundException(nameof(Pet), request.PetId);

            return _mapper.Map<PetDetailVm>(pet);
        }
    }
}

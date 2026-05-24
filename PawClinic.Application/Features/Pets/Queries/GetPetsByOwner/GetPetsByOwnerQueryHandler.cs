using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;

namespace PawClinic.Application.Features.Pets.Queries.GetPetsByOwner
{
    public class GetPetsByOwnerQueryHandler : IRequestHandler<GetPetsByOwnerQuery, List<PetListVm>>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public GetPetsByOwnerQueryHandler(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<List<PetListVm>> Handle(GetPetsByOwnerQuery request, CancellationToken cancellationToken)
        {
            var pets = await _petRepository.GetByOwnerAsync(request.OwnerId);
            return _mapper.Map<List<PetListVm>>(pets);
        }
    }
}

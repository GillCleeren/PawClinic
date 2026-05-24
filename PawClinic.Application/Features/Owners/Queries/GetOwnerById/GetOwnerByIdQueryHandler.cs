using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Exceptions;
using PawClinic.Domain.Entities;

namespace PawClinic.Application.Features.Owners.Queries.GetOwnerById
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDetailVm>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IPetRepository petRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<OwnerDetailVm> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.OwnerId);
            if (owner == null)
                throw new NotFoundException(nameof(Owner), request.OwnerId);

            var vm = _mapper.Map<OwnerDetailVm>(owner);
            var pets = await _petRepository.GetByOwnerAsync(request.OwnerId);
            vm.Pets = _mapper.Map<List<PetSummaryDto>>(pets);

            return vm;
        }
    }
}

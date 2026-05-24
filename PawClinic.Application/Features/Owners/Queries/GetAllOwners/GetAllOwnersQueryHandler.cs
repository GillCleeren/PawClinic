using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;

namespace PawClinic.Application.Features.Owners.Queries.GetAllOwners
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, PagedOwnerListVm>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public GetAllOwnersQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<PagedOwnerListVm> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            var allOwners = await _ownerRepository.ListAllAsync();
            var totalCount = allOwners.Count;
            var paged = allOwners
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToList();

            return new PagedOwnerListVm
            {
                Owners = _mapper.Map<List<OwnerListVm>>(paged),
                TotalCount = totalCount,
                Page = request.Page,
                Size = request.Size
            };
        }
    }
}

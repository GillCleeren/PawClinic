using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;

namespace PawClinic.Application.Features.Vets.Queries.GetAllVets
{
    public class GetAllVetsQueryHandler : IRequestHandler<GetAllVetsQuery, List<VetListVm>>
    {
        private readonly IVetRepository _vetRepository;
        private readonly IMapper _mapper;

        public GetAllVetsQueryHandler(IVetRepository vetRepository, IMapper mapper)
        {
            _vetRepository = vetRepository;
            _mapper = mapper;
        }

        public async Task<List<VetListVm>> Handle(GetAllVetsQuery request, CancellationToken cancellationToken)
        {
            var vets = await _vetRepository.ListAllAsync();
            return _mapper.Map<List<VetListVm>>(vets);
        }
    }
}

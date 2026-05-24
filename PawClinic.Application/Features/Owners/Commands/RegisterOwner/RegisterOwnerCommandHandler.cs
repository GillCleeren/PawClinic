using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;

namespace PawClinic.Application.Features.Owners.Commands.RegisterOwner
{
    public class RegisterOwnerCommandHandler : IRequestHandler<RegisterOwnerCommand, RegisterOwnerCommandResponse>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public RegisterOwnerCommandHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<RegisterOwnerCommandResponse> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
        {
            var response = new RegisterOwnerCommandResponse();

            var validator = new RegisterOwnerCommandValidator(_ownerRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var owner = _mapper.Map<Owner>(request);
            owner = await _ownerRepository.AddAsync(owner);
            response.Owner = _mapper.Map<OwnerDto>(owner);

            return response;
        }
    }
}

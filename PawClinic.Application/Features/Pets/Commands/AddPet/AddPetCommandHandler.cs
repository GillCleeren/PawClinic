using AutoMapper;
using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;

namespace PawClinic.Application.Features.Pets.Commands.AddPet
{
    public class AddPetCommandHandler : IRequestHandler<AddPetCommand, AddPetCommandResponse>
    {
        private readonly IPetRepository _petRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public AddPetCommandHandler(IPetRepository petRepository, IOwnerRepository ownerRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<AddPetCommandResponse> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            var response = new AddPetCommandResponse();

            var validator = new AddPetCommandValidator(_ownerRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var pet = _mapper.Map<Pet>(request);
            pet = await _petRepository.AddAsync(pet);
            response.Pet = _mapper.Map<PetDto>(pet);

            return response;
        }
    }
}

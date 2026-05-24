using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Exceptions;
using PawClinic.Domain.Entities;

namespace PawClinic.Application.Features.Pets.Commands.ArchivePet
{
    public class ArchivePetCommandHandler : IRequestHandler<ArchivePetCommand, Unit>
    {
        private readonly IPetRepository _petRepository;

        public ArchivePetCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Unit> Handle(ArchivePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetByIdAsync(request.PetId);
            if (pet == null)
                throw new NotFoundException(nameof(Pet), request.PetId);

            pet.IsArchived = true;
            await _petRepository.UpdateAsync(pet);

            return Unit.Value;
        }
    }
}

using MediatR;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Application.Exceptions;
using PawClinic.Domain.Entities;

namespace PawClinic.Application.Features.Owners.Commands.UpdateOwnerContact
{
    public class UpdateOwnerContactCommandHandler : IRequestHandler<UpdateOwnerContactCommand, Unit>
    {
        private readonly IOwnerRepository _ownerRepository;

        public UpdateOwnerContactCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Unit> Handle(UpdateOwnerContactCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOwnerContactCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var owner = await _ownerRepository.GetByIdAsync(request.OwnerId);
            if (owner == null)
                throw new NotFoundException(nameof(Owner), request.OwnerId);

            owner.Email = request.Email;
            owner.PhoneNumber = request.PhoneNumber;
            owner.Address = request.Address;

            await _ownerRepository.UpdateAsync(owner);

            return Unit.Value;
        }
    }
}

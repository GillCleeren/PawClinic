using FluentValidation;
using PawClinic.Application.Contracts.Persistence;

namespace PawClinic.Application.Features.Pets.Commands.AddPet
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        private readonly IOwnerRepository _ownerRepository;

        public AddPetCommandValidator(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .LessThan(DateTime.UtcNow).WithMessage("{PropertyName} cannot be in the future.");

            RuleFor(p => p.OwnerId)
                .MustAsync(OwnerExistsAsync).WithMessage("The specified owner does not exist.");
        }

        private async Task<bool> OwnerExistsAsync(Guid ownerId, CancellationToken token)
        {
            var owner = await _ownerRepository.GetByIdAsync(ownerId);
            return owner != null;
        }
    }
}

using FluentValidation;
using PawClinic.Application.Contracts.Persistence;

namespace PawClinic.Application.Features.Owners.Commands.RegisterOwner
{
    public class RegisterOwnerCommandValidator : AbstractValidator<RegisterOwnerCommand>
    {
        private readonly IOwnerRepository _ownerRepository;

        public RegisterOwnerCommandValidator(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.")
                .MustAsync(BeUniqueEmail).WithMessage("An owner with this email already exists.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken token)
        {
            return await _ownerRepository.IsEmailUniqueAsync(email);
        }
    }
}

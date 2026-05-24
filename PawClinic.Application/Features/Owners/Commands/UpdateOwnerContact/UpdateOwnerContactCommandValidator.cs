using FluentValidation;

namespace PawClinic.Application.Features.Owners.Commands.UpdateOwnerContact
{
    public class UpdateOwnerContactCommandValidator : AbstractValidator<UpdateOwnerContactCommand>
    {
        public UpdateOwnerContactCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}

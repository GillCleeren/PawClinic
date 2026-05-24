using MediatR;

namespace PawClinic.Application.Features.Owners.Commands.UpdateOwnerContact
{
    public class UpdateOwnerContactCommand : IRequest<Unit>
    {
        public Guid OwnerId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

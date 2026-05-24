using PawClinic.Application.Responses;

namespace PawClinic.Application.Features.Owners.Commands.RegisterOwner
{
    public class RegisterOwnerCommandResponse : BaseResponse
    {
        public RegisterOwnerCommandResponse() : base() { }
        public OwnerDto Owner { get; set; } = default!;
    }
}

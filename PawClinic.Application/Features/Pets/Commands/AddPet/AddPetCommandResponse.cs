using PawClinic.Application.Responses;

namespace PawClinic.Application.Features.Pets.Commands.AddPet
{
    public class AddPetCommandResponse : BaseResponse
    {
        public AddPetCommandResponse() : base() { }
        public PetDto Pet { get; set; } = default!;
    }
}

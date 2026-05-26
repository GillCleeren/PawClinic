using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Contracts;

public interface IPetDataService
{
    Task<List<PetViewModel>> GetPetsByOwner(Guid ownerId);
    Task<ApiResponse<Guid>> AddPet(PetViewModel pet, int species);
}

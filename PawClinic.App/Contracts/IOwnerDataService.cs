using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Contracts;

public interface IOwnerDataService
{
    Task<List<OwnerViewModel>> GetAllOwners(int page = 1, int size = 10);
    Task<OwnerViewModel?> GetOwnerById(Guid id);
    Task<ApiResponse<Guid>> RegisterOwner(OwnerViewModel ownerViewModel);
    Task<ApiResponse<Guid>> UpdateOwnerContact(OwnerViewModel ownerViewModel);
}

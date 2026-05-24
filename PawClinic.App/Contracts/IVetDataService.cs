using PawClinic.App.ViewModels;

namespace PawClinic.App.Contracts;

public interface IVetDataService
{
    Task<List<VetViewModel>> GetAllVets();
}

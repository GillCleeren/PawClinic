using AutoMapper;
using Blazored.LocalStorage;
using PawClinic.App.Contracts;
using PawClinic.App.Profiles;
using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;
using System.Net.Http.Json;

namespace PawClinic.App.Services;

public class VetDataService : BaseDataService, IVetDataService
{
    private readonly IMapper _mapper;

    public VetDataService(HttpClient httpClient, IMapper mapper, ILocalStorageService localStorage)
        : base(httpClient, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<List<VetViewModel>> GetAllVets()
    {
        await AddBearerToken();

        var vets = await _httpClient.GetFromJsonAsync<List<Mappings.VetListDto>>("/api/vets");
        if (vets is null)
            return new List<VetViewModel>();

        return _mapper.Map<List<VetViewModel>>(vets);
    }
}

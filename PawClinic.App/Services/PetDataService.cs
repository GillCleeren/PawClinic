using AutoMapper;
using Blazored.LocalStorage;
using PawClinic.App.Contracts;
using PawClinic.App.Profiles;
using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;
using System.Net.Http.Json;

namespace PawClinic.App.Services;

public class PetDataService : BaseDataService, IPetDataService
{
    private readonly IMapper _mapper;

    public PetDataService(HttpClient httpClient, IMapper mapper, ILocalStorageService localStorage)
        : base(httpClient, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<List<PetViewModel>> GetPetsByOwner(Guid ownerId)
    {
        await AddBearerToken();

        var pets = await _httpClient.GetFromJsonAsync<List<Mappings.PetListDto>>($"/api/pets/owner/{ownerId}");
        return pets is null ? new List<PetViewModel>() : _mapper.Map<List<PetViewModel>>(pets);
    }

    public async Task<ApiResponse<Guid>> AddPet(PetViewModel pet, int species)
    {
        await AddBearerToken();

        try
        {
            var request = new AddPetRequest(pet.Name, species, pet.Breed, pet.DateOfBirth, pet.OwnerId);
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/pets", request);

            if (httpResponse.IsSuccessStatusCode)
                return new ApiResponse<Guid> { Success = true };

            var error = await httpResponse.Content.ReadAsStringAsync();
            return new ApiResponse<Guid> { Success = false, Message = error };
        }
        catch (HttpRequestException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    private record AddPetRequest(string Name, int Species, string? Breed, DateTime DateOfBirth, Guid OwnerId);
}

using AutoMapper;
using Blazored.LocalStorage;
using PawClinic.App.Contracts;
using PawClinic.App.Profiles;
using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;
using System.Net.Http.Json;

namespace PawClinic.App.Services;

public class OwnerDataService : BaseDataService, IOwnerDataService
{
    private readonly IMapper _mapper;

    public OwnerDataService(HttpClient httpClient, IMapper mapper, ILocalStorageService localStorage)
        : base(httpClient, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<List<OwnerViewModel>> GetAllOwners(int page = 1, int size = 10)
    {
        await AddBearerToken();

        var response = await _httpClient.GetFromJsonAsync<PagedOwnerListResponse>(
            $"/api/owners?page={page}&size={size}");

        if (response?.Owners is null)
            return new List<OwnerViewModel>();

        return _mapper.Map<List<OwnerViewModel>>(response.Owners);
    }

    public async Task<OwnerViewModel?> GetOwnerById(Guid id)
    {
        await AddBearerToken();

        var response = await _httpClient.GetFromJsonAsync<OwnerDetailResponse>($"/api/owners/{id}");
        return response is null ? null : _mapper.Map<OwnerViewModel>(response);
    }

    public async Task<ApiResponse<Guid>> RegisterOwner(OwnerViewModel ownerViewModel)
    {
        await AddBearerToken();

        try
        {
            var command = new RegisterOwnerRequest(ownerViewModel.Name, ownerViewModel.Email, ownerViewModel.PhoneNumber, ownerViewModel.Address);
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/owners", command);

            if (httpResponse.IsSuccessStatusCode)
            {
                return new ApiResponse<Guid> { Success = true };
            }

            var error = await httpResponse.Content.ReadAsStringAsync();
            return new ApiResponse<Guid> { Success = false, Message = error };
        }
        catch (HttpRequestException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<ApiResponse<Guid>> UpdateOwnerContact(OwnerViewModel ownerViewModel)
    {
        await AddBearerToken();

        try
        {
            var command = new UpdateOwnerContactRequest(ownerViewModel.Email, ownerViewModel.PhoneNumber, ownerViewModel.Address);
            var httpResponse = await _httpClient.PutAsJsonAsync(
                $"/api/owners/{ownerViewModel.OwnerId}/contact", command);

            if (httpResponse.IsSuccessStatusCode)
            {
                return new ApiResponse<Guid> { Success = true };
            }

            var error = await httpResponse.Content.ReadAsStringAsync();
            return new ApiResponse<Guid> { Success = false, Message = error };
        }
        catch (HttpRequestException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    // Local DTOs matching the API response shape
    private record PagedOwnerListResponse(List<Mappings.OwnerListDto> Owners, int TotalCount, int Page, int Size);
    private record OwnerDetailResponse(Guid OwnerId, string Name, string Email, string PhoneNumber, string Address, List<object> Pets);
    private record RegisterOwnerRequest(string Name, string Email, string PhoneNumber, string Address);
    private record UpdateOwnerContactRequest(string Email, string PhoneNumber, string Address);
}

using AutoMapper;
using Blazored.LocalStorage;
using PawClinic.App.Contracts;
using PawClinic.App.Profiles;
using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;
using System.Net.Http.Json;

namespace PawClinic.App.Services;

public class AppointmentDataService : BaseDataService, IAppointmentDataService
{
    private readonly IMapper _mapper;

    public AppointmentDataService(HttpClient httpClient, IMapper mapper, ILocalStorageService localStorage)
        : base(httpClient, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<List<AppointmentListViewModel>> GetUpcomingAppointments()
    {
        await AddBearerToken();

        var appointments = await _httpClient.GetFromJsonAsync<List<Mappings.AppointmentListDto>>("/api/appointments/upcoming");
        if (appointments is null)
            return new List<AppointmentListViewModel>();

        return _mapper.Map<List<AppointmentListViewModel>>(appointments);
    }

    public async Task<List<AppointmentListViewModel>> GetAppointmentHistory(Guid petId)
    {
        await AddBearerToken();

        var appointments = await _httpClient.GetFromJsonAsync<List<Mappings.AppointmentListDto>>(
            $"/api/appointments/history/{petId}");
        if (appointments is null)
            return new List<AppointmentListViewModel>();

        return _mapper.Map<List<AppointmentListViewModel>>(appointments);
    }

    public async Task<AppointmentDetailViewModel?> GetAppointmentById(Guid id)
    {
        await AddBearerToken();

        var dto = await _httpClient.GetFromJsonAsync<AppointmentDetailDto>($"/api/appointments/{id}");
        if (dto is null) return null;

        return new AppointmentDetailViewModel
        {
            AppointmentId = dto.AppointmentId,
            ScheduledDateTime = dto.ScheduledDateTime,
            ReasonForVisit = dto.ReasonForVisit,
            Status = StatusName(dto.Status),
            Notes = dto.Notes,
            PetId = dto.Pet.PetId,
            PetName = dto.Pet.Name,
            PetSpecies = SpeciesName(dto.Pet.Species),
            VetId = dto.Vet.VetId,
            VetName = dto.Vet.Name,
            VetSpecialisation = SpecialisationName(dto.Vet.Specialisation)
        };
    }

    public async Task<ApiResponse<Guid>> ScheduleAppointment(Guid petId, Guid vetId, DateTime scheduledDateTime, string reasonForVisit)
    {
        await AddBearerToken();

        try
        {
            var request = new ScheduleRequest(petId, vetId, scheduledDateTime, reasonForVisit);
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/appointments", request);

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

    public async Task<ApiResponse<bool>> CancelAppointment(Guid id, string? reason)
    {
        await AddBearerToken();

        try
        {
            var httpResponse = await _httpClient.PutAsJsonAsync(
                $"/api/appointments/{id}/cancel", new { reason });

            if (httpResponse.IsSuccessStatusCode)
                return new ApiResponse<bool> { Success = true };

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return new ApiResponse<bool> { Success = false, Message = "You must be logged in to cancel appointments." };

            var error = await httpResponse.Content.ReadAsStringAsync();
            return new ApiResponse<bool> { Success = false, Message = string.IsNullOrWhiteSpace(error) ? "An error occurred. Please try again." : error };
        }
        catch (HttpRequestException ex)
        {
            return ConvertApiExceptions<bool>(ex);
        }
    }

    public async Task<ApiResponse<bool>> CompleteAppointment(Guid id, string notes)
    {
        await AddBearerToken();

        try
        {
            var httpResponse = await _httpClient.PutAsJsonAsync(
                $"/api/appointments/{id}/complete", new { notes });

            if (httpResponse.IsSuccessStatusCode)
                return new ApiResponse<bool> { Success = true };

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return new ApiResponse<bool> { Success = false, Message = "You must be logged in to complete appointments." };

            var error = await httpResponse.Content.ReadAsStringAsync();
            return new ApiResponse<bool> { Success = false, Message = string.IsNullOrWhiteSpace(error) ? "An error occurred. Please try again." : error };
        }
        catch (HttpRequestException ex)
        {
            return ConvertApiExceptions<bool>(ex);
        }
    }

    private static string StatusName(int value) => value switch
    {
        0 => "Scheduled",
        1 => "Completed",
        2 => "Cancelled",
        _ => "Unknown"
    };

    private static string SpeciesName(int value) => value switch
    {
        0 => "Dog",
        1 => "Cat",
        2 => "Rabbit",
        3 => "Bird",
        _ => "Other"
    };

    private static string SpecialisationName(int value) => value switch
    {
        0 => "General Practice",
        1 => "Surgery",
        2 => "Dentistry",
        3 => "Dermatology",
        _ => "Unknown"
    };

    // Local DTOs matching API response shapes
    private record ScheduleRequest(Guid PetId, Guid VetId, DateTime ScheduledDateTime, string ReasonForVisit);
    private record AppointmentDetailDto(
        Guid AppointmentId,
        DateTime ScheduledDateTime,
        string ReasonForVisit,
        int Status,
        string? Notes,
        PetInfoDto Pet,
        VetInfoDto Vet);
    private record PetInfoDto(Guid PetId, string Name, int Species);
    private record VetInfoDto(Guid VetId, string Name, int Specialisation);
}

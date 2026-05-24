using AutoMapper;
using Blazored.LocalStorage;
using PawClinic.App.Contracts;
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

        var appointments = await _httpClient.GetFromJsonAsync<List<AppointmentListDto>>("/api/appointments/upcoming");
        if (appointments is null)
            return new List<AppointmentListViewModel>();

        return _mapper.Map<List<AppointmentListViewModel>>(appointments);
    }

    public async Task<List<AppointmentListViewModel>> GetAppointmentHistory(Guid petId)
    {
        await AddBearerToken();

        var appointments = await _httpClient.GetFromJsonAsync<List<AppointmentListDto>>(
            $"/api/appointments/history/{petId}");
        if (appointments is null)
            return new List<AppointmentListViewModel>();

        return _mapper.Map<List<AppointmentListViewModel>>(appointments);
    }

    private record AppointmentListDto(
        Guid AppointmentId,
        DateTime ScheduledDateTime,
        string ReasonForVisit,
        int Status,
        string PetName,
        string VetName);
}

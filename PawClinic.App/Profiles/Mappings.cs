using AutoMapper;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Profiles;

public class Mappings : Profile
{
    public Mappings()
    {
        // Owner mappings — API list response to ViewModel
        CreateMap<OwnerListDto, OwnerViewModel>()
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<OwnerDetailDto, OwnerViewModel>().ReverseMap();

        // Vet mappings
        CreateMap<VetListDto, VetViewModel>()
            .ForMember(dest => dest.Specialisation, opt => opt.MapFrom(src => src.Specialisation.ToString()))
            .ReverseMap();

        // Appointment mappings
        CreateMap<AppointmentListDto, AppointmentListViewModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();
    }

    // Internal DTOs matching the API JSON response shapes
    public record OwnerListDto(Guid OwnerId, string Name, string Email, string PhoneNumber);
    public record OwnerDetailDto(Guid OwnerId, string Name, string Email, string PhoneNumber, string Address);
    public record VetListDto(Guid VetId, string Name, int Specialisation);
    public record AppointmentListDto(
        Guid AppointmentId,
        DateTime ScheduledDateTime,
        string ReasonForVisit,
        int Status,
        string PetName,
        string VetName);
}

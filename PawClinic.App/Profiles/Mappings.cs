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

        // Pet mappings
        CreateMap<PetListDto, PetViewModel>()
            .ForMember(dest => dest.Species, opt => opt.MapFrom(src => SpeciesName(src.Species)));
    }

    private static string SpeciesName(int value) => value switch
    {
        0 => "Dog",
        1 => "Cat",
        2 => "Rabbit",
        3 => "Bird",
        _ => "Other"
    };

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
    public record PetListDto(Guid PetId, string Name, int Species, string? Breed, bool IsArchived);
}

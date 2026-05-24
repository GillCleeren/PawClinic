using AutoMapper;
using PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments;
using PawClinic.Application.Features.Owners.Commands.RegisterOwner;
using PawClinic.Application.Features.Owners.Queries.GetAllOwners;
using PawClinic.Application.Features.Owners.Queries.GetOwnerById;
using PawClinic.Application.Features.Pets.Queries.GetPetById;
using PawClinic.Application.Features.Pets.Queries.GetPetsByOwner;
using PawClinic.Application.Features.Vets.Queries.GetAllVets;
using PawClinic.Domain.Entities;

// Aliases to resolve PetDto / VetDto ambiguity across feature namespaces
using AddPetDto = PawClinic.Application.Features.Pets.Commands.AddPet.PetDto;
using AppointmentPetDto = PawClinic.Application.Features.Appointments.Queries.GetAppointmentById.PetDto;
using AppointmentVetDto = PawClinic.Application.Features.Appointments.Queries.GetAppointmentById.VetDto;
using AppointmentDetailVm = PawClinic.Application.Features.Appointments.Queries.GetAppointmentById.AppointmentDetailVm;

namespace PawClinic.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Owners
            CreateMap<Owner, OwnerDto>();
            CreateMap<Owner, OwnerDetailVm>();
            CreateMap<Owner, OwnerListVm>();
            CreateMap<RegisterOwnerCommand, Owner>();

            // Pets
            CreateMap<Pet, PetSummaryDto>();
            CreateMap<Pet, AddPetDto>();
            CreateMap<Pet, PetDetailVm>()
                .ForMember(d => d.OwnerName, opt => opt.Ignore());
            CreateMap<Pet, PetListVm>();
            CreateMap<Features.Pets.Commands.AddPet.AddPetCommand, Pet>();

            // Vets
            CreateMap<Vet, VetListVm>();
            CreateMap<Vet, AppointmentVetDto>();

            // Appointments
            CreateMap<Pet, AppointmentPetDto>();
            CreateMap<Appointment, AppointmentDetailVm>();
            CreateMap<Appointment, AppointmentListVm>()
                .ForMember(d => d.PetName, opt => opt.Ignore())
                .ForMember(d => d.VetName, opt => opt.Ignore());
            CreateMap<Features.Appointments.Commands.ScheduleAppointment.ScheduleAppointmentCommand, Appointment>();
        }
    }
}

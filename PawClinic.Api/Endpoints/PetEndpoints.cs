using MediatR;
using PawClinic.Application.Features.Pets.Commands.AddPet;
using PawClinic.Application.Features.Pets.Commands.ArchivePet;
using PawClinic.Application.Features.Pets.Queries.GetPetById;
using PawClinic.Application.Features.Pets.Queries.GetPetsByOwner;

namespace PawClinic.Api.Endpoints
{
    public static class PetEndpoints
    {
        public static void MapPetEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/pets")
                .WithTags("Pets")
                .RequireAuthorization();

            group.MapGet("/{id:guid}", GetPetById)
                .WithName("GetPetById");

            group.MapGet("/owner/{ownerId:guid}", GetPetsByOwner)
                .WithName("GetPetsByOwner");

            group.MapPost("/", AddPet)
                .WithName("AddPet");

            group.MapPut("/{id:guid}/archive", ArchivePet)
                .WithName("ArchivePet");
        }

        private static async Task<IResult> GetPetById(Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new GetPetByIdQuery { PetId = id });
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> GetPetsByOwner(Guid ownerId, IMediator mediator)
        {
            var result = await mediator.Send(new GetPetsByOwnerQuery { OwnerId = ownerId });
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> AddPet(AddPetCommand command, IMediator mediator)
        {
            var result = await mediator.Send(command);
            if (!result.Success)
                return TypedResults.BadRequest(result);
            return TypedResults.Created($"/api/pets/{result.Pet.PetId}", result);
        }

        private static async Task<IResult> ArchivePet(Guid id, IMediator mediator)
        {
            await mediator.Send(new ArchivePetCommand { PetId = id });
            return TypedResults.NoContent();
        }
    }
}

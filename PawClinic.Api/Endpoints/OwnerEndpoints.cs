using MediatR;
using Microsoft.AspNetCore.Mvc;
using PawClinic.Application.Features.Owners.Commands.RegisterOwner;
using PawClinic.Application.Features.Owners.Commands.UpdateOwnerContact;
using PawClinic.Application.Features.Owners.Queries.GetAllOwners;
using PawClinic.Application.Features.Owners.Queries.GetOwnerById;

namespace PawClinic.Api.Endpoints
{
    public static class OwnerEndpoints
    {
        public static void MapOwnerEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/owners")
                .WithTags("Owners")
                .RequireAuthorization();

            group.MapGet("/", GetAllOwners)
                .WithName("GetAllOwners");

            group.MapGet("/{id:guid}", GetOwnerById)
                .WithName("GetOwnerById");

            group.MapPost("/", RegisterOwner)
                .WithName("RegisterOwner");

            group.MapPut("/{id:guid}/contact", UpdateOwnerContact)
                .WithName("UpdateOwnerContact");
        }

        private static async Task<IResult> GetAllOwners(
            IMediator mediator,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var result = await mediator.Send(new GetAllOwnersQuery { Page = page, Size = size });
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> GetOwnerById(
            Guid id,
            IMediator mediator)
        {
            var result = await mediator.Send(new GetOwnerByIdQuery { OwnerId = id });
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> RegisterOwner(
            RegisterOwnerCommand command,
            IMediator mediator)
        {
            var result = await mediator.Send(command);
            if (!result.Success)
                return TypedResults.BadRequest(result);
            return TypedResults.Created($"/api/owners/{result.Owner.OwnerId}", result);
        }

        private static async Task<IResult> UpdateOwnerContact(
            Guid id,
            UpdateOwnerContactCommand command,
            IMediator mediator)
        {
            command.OwnerId = id;
            await mediator.Send(command);
            return TypedResults.NoContent();
        }
    }
}

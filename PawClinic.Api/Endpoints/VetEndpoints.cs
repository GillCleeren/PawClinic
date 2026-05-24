using MediatR;
using Microsoft.AspNetCore.Mvc;
using PawClinic.Application.Features.Vets.Queries.GetAllVets;
using PawClinic.Application.Features.Vets.Queries.GetVetSchedule;

namespace PawClinic.Api.Endpoints
{
    public static class VetEndpoints
    {
        public static void MapVetEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/vets")
                .WithTags("Vets");

            group.MapGet("/", GetAllVets)
                .WithName("GetAllVets");

            group.MapGet("/{id:guid}/schedule", GetVetSchedule)
                .WithName("GetVetSchedule");
        }

        private static async Task<IResult> GetAllVets(IMediator mediator)
        {
            var result = await mediator.Send(new GetAllVetsQuery());
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> GetVetSchedule(
            Guid id,
            IMediator mediator,
            [FromQuery] DateOnly? date = null)
        {
            var result = await mediator.Send(new GetVetScheduleQuery
            {
                VetId = id,
                Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow)
            });
            return TypedResults.Ok(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PawClinic.Application.Features.Appointments.Commands.CancelAppointment;
using PawClinic.Application.Features.Appointments.Commands.CompleteAppointment;
using PawClinic.Application.Features.Appointments.Commands.ScheduleAppointment;
using PawClinic.Application.Features.Appointments.Queries.GetAppointmentById;
using PawClinic.Application.Features.Appointments.Queries.GetAppointmentHistory;
using PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments;

namespace PawClinic.Api.Endpoints
{
    public static class AppointmentEndpoints
    {
        public static void MapAppointmentEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/appointments")
                .WithTags("Appointments")
                .RequireAuthorization();

            group.MapGet("/{id:guid}", GetAppointmentById)
                .WithName("GetAppointmentById");

            group.MapGet("/upcoming", GetUpcomingAppointments)
                .WithName("GetUpcomingAppointments");

            group.MapGet("/history/{petId:guid}", GetAppointmentHistory)
                .WithName("GetAppointmentHistory");

            group.MapPost("/", ScheduleAppointment)
                .WithName("ScheduleAppointment");

            group.MapPut("/{id:guid}/cancel", CancelAppointment)
                .WithName("CancelAppointment");

            group.MapPut("/{id:guid}/complete", CompleteAppointment)
                .WithName("CompleteAppointment");
        }

        private static async Task<IResult> GetAppointmentById(Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new GetAppointmentByIdQuery { AppointmentId = id });
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> GetUpcomingAppointments(
            IMediator mediator,
            [FromQuery] Guid? vetId = null,
            [FromQuery] Guid? petId = null)
        {
            var result = await mediator.Send(new GetUpcomingAppointmentsQuery { VetId = vetId, PetId = petId });
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> GetAppointmentHistory(Guid petId, IMediator mediator)
        {
            var result = await mediator.Send(new GetAppointmentHistoryQuery { PetId = petId });
            return TypedResults.Ok(result);
        }

        private static async Task<IResult> ScheduleAppointment(
            ScheduleAppointmentCommand command,
            IMediator mediator)
        {
            var id = await mediator.Send(command);
            return TypedResults.Created($"/api/appointments/{id}", id);
        }

        private static async Task<IResult> CancelAppointment(
            Guid id,
            CancelAppointmentCommand command,
            IMediator mediator)
        {
            command.AppointmentId = id;
            await mediator.Send(command);
            return TypedResults.NoContent();
        }

        private static async Task<IResult> CompleteAppointment(
            Guid id,
            CompleteAppointmentCommand command,
            IMediator mediator)
        {
            command.AppointmentId = id;
            await mediator.Send(command);
            return TypedResults.NoContent();
        }
    }
}

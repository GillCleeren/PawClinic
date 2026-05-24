using PawClinic.Application.Contracts.Identity;
using PawClinic.Application.Models.Authentication;

namespace PawClinic.Api.Endpoints
{
    public static class AccountEndpoints
    {
        public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/account")
                .WithTags("Account");

            group.MapPost("/authenticate", Authenticate)
                .WithName("Authenticate")
                .AllowAnonymous();

            group.MapPost("/register", Register)
                .WithName("Register")
                .AllowAnonymous();
        }

        private static async Task<IResult> Authenticate(
            AuthenticationRequest request,
            IAuthenticationService authService)
        {
            var response = await authService.AuthenticateAsync(request);
            return TypedResults.Ok(response);
        }

        private static async Task<IResult> Register(
            RegistrationRequest request,
            IAuthenticationService authService)
        {
            var response = await authService.RegisterAsync(request);
            return TypedResults.Ok(response);
        }
    }
}

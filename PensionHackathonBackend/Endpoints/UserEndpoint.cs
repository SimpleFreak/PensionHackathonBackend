using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PensionHackathonBackend.Application.Services;
using PensionHackathonBackend.Contracts.Users;
using System;
using System.Threading.Tasks;

namespace PensionHackathonBackend.Endpoints
{
    public static class UserEndpoint
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("Registration", Registration);
            app.MapPost("Authorization", Authorization);
            app.MapDelete("Delete", Delete);

            return app;
        }

        private static async Task<IResult> Registration(
            [FromBody] RegisterUserRequest request,
            UserService userService)
        {
            await userService.RegistrationUser(request.Login,
                request.Password, request.Role);

            return Results.Ok("Registration");
        }

        private static async Task<IResult> Authorization(
            [FromBody] LoginUserRequest request,
            UserService userService,
            HttpContext context)
        {
            var token = await userService.AuthorizationUser(request.Login,
                request.Password);

            context.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok();
        }

        private static async Task<IResult> Delete(Guid id, UserService userService)
        {
            return Results.Ok(await userService.DeleteUser(id));
        }
    }
}

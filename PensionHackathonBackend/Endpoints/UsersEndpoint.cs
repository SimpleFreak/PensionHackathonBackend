using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PensionHackathonBackend.Application.Services;
using PensionHackathonBackend.Contracts.UserRequests;

namespace PensionHackathonBackend.Endpoints;

/* Класс эндпоинта для пользователя */
public static class UsersEndpoint
{
    /* Добавление методов для отображения запросов к пользователю */
    public static IEndpointRouteBuilder AddUsersEndpoints(this
        IEndpointRouteBuilder app)
    {
        app.MapPost("Register", Registration);
        app.MapPost("Login", Authorization);
        app.MapDelete("Delete", Delete);

        return app;
    }

    /* Регистрация пользователя в системе */
    private static async Task<IResult> Registration(
        [FromBody]RegisterUserRequest request,
        UserService userService)
    {
        await userService.Register(request.Login, request.Password, request.Role);

        return Results.Ok("Register");
    }

    /* Авторизация пользователя в системе */
    private static async Task<IResult> Authorization(
        [FromBody]LoginUserRequest request,
        UserService userService,
        HttpContext context)
    {
        var token = await userService.Login(request.Login, request.Password);

        context.Response.Cookies.Append("tasty-cookies", token);

        return Results.Ok();
    }

    /* Удаление пользователя */
    private static async Task<IResult> Delete(int id, UserService userService, HttpContext context)
    {
        return Results.Ok(await userService.DeleteUser(id));
    }
}
using System;

namespace PensionHackathonBackend.Contracts.UserResponse;

/* Класс для ответа пользователя */
public record UserResponse(
    Guid Id,
    string Login,
    string Role,
    string Password
);
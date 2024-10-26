using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionHackathonBackend.Contracts.UserRequests;

/* Класс Авторизации пользователя */
public record LoginUserRequest(
    [Required, NotNull] string Login,
    [Required, NotNull] string Password
);
namespace PensionHackathonBackend.Contracts.UserResponse;

/* Класс для запроса пользователя */
public record UserRequest(
    string Login,
    string Password,
    string Role
);
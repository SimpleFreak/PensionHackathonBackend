using System;

namespace PensionHackathonBackend.AdminPanel.Contract
{
    /* Запрос пользователя для панели администратора */
    public record UserRequest(
        Guid Id,
        string Login,
        string Role,
        string Password = ""
    );
}

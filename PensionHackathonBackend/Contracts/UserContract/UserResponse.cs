using System;

namespace PensionHackathonBackend.Contracts.UserContract
{
    public record UserResponse(
        Guid Id,
        string Login,
        string Role,
        string Password
    );
}

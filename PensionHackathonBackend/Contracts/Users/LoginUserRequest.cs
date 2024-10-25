using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionHackathonBackend.Contracts.Users
{
    public record LoginUserRequest(
        [Required, NotNull] string Login,
        [Required, NotNull] string Password
    );
}

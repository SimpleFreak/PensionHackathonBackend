using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionHackathonBackend.Contracts.UserRequests;

/* ����� ����������� ������������ */
public record RegisterUserRequest(
    [Required, NotNull] string Login,
    [Required, NotNull] string Password,
    [Required, NotNull] string Role
);
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionHackathonBackend.Contracts.UserRequests;

/* ����� ����������� ������������ */
public record LoginUserRequest(
    [Required, NotNull] string Login,
    [Required, NotNull] string Password
);
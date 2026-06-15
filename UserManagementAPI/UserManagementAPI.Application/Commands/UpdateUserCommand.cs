namespace UserManagementAPI.Application.Commands;

public record UpdateUserCommand(string? Name, string? Username, string? Password);

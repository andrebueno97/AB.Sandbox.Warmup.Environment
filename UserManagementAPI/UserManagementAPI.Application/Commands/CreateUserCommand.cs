namespace UserManagementAPI.Application.Commands;

public record CreateUserCommand(string Name, string Username, string Password);

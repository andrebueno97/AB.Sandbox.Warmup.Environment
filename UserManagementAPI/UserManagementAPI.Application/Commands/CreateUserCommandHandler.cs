namespace UserManagementAPI.Application.Commands;

using UserManagementAPI.Domain.Entities;
using UserManagementAPI.Domain.Ports;

public class CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Guid> HandleAsync(CreateUserCommand command)
    {
        var user = new User
        {
            Name = command.Name,
            Username = command.Username,
            PasswordHash = _passwordHasher.Hash(command.Password)
        };

        return await _userRepository.CreateAsync(user);
    }
}

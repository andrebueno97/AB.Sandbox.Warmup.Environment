namespace UserManagementAPI.Application.Commands;

using UserManagementAPI.Domain.Entities;
using UserManagementAPI.Domain.Ports;

public class UpdateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<User?> HandleAsync(Guid id, UpdateUserCommand command)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return null;
        }

        if (command.Name is not null)
        {
            user.Name = command.Name;
        }

        if (command.Username is not null)
        {
            user.Username = command.Username;
        }

        if (command.Password is not null)
        {
            user.PasswordHash = _passwordHasher.Hash(command.Password);
        }

        await _userRepository.UpdateAsync(user);
        return user;
    }
}

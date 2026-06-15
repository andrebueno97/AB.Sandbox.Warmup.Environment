namespace UserManagementAPI.Application.Commands;

using UserManagementAPI.Domain.Ports;

public class DeleteUserCommandHandler(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<bool> HandleAsync(DeleteUserCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.Id);
        if (user is null)
        {
            return false;
        }

        await _userRepository.DeleteAsync(command.Id);
        return true;
    }
}

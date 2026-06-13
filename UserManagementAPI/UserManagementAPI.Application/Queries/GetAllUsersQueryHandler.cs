namespace UserManagementAPI.Application.Queries;

using UserManagementAPI.Domain.Entities;
using UserManagementAPI.Domain.Ports;

public class GetAllUsersQueryHandler(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<IEnumerable<User>> HandleAsync(GetAllUsersQuery query)
    {
        return await _userRepository.GetAllAsync();
    }
}

namespace UserManagementAPI.Application.Queries;

using UserManagementAPI.Domain.Entities;
using UserManagementAPI.Domain.Ports;

public class GetUserQueryHandler(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User?> HandleAsync(GetUserQuery query)
    {
        return await _userRepository.GetByIdAsync(query.Id);
    }
}

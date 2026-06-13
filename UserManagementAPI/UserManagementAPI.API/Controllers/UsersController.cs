namespace UserManagementAPI.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Application.Commands;
using UserManagementAPI.Application.Queries;

[ApiController]
[Route("api/[controller]")]
public class UsersController(CreateUserCommandHandler createUserCommandHandler, GetAllUsersQueryHandler getAllUsersQueryHandler) : ControllerBase
{
    private readonly CreateUserCommandHandler _createUserCommandHandler = createUserCommandHandler;
    private readonly GetAllUsersQueryHandler _getAllUsersQueryHandler = getAllUsersQueryHandler;

    /// <summary>
    /// Cria um novo usuário no sistema.
    /// </summary>
    /// <param name="command">Dados do usuário (Name, Username, Password)</param>
    /// <returns>ID do usuário criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _createUserCommandHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
    }

    /// <summary>
    /// Obtém todos os usuários cadastrados.
    /// </summary>
    /// <returns>Lista de usuários</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
    {
        var users = await _getAllUsersQueryHandler.HandleAsync(new GetAllUsersQuery());
        var userDtos = users.Select(u => new { u.Id, u.Name, u.Username, u.CreatedAt, u.UpdatedAt });
        return Ok(userDtos);
    }

    /// <summary>
    /// Obtém um usuário específico (placeholder para futura implementação).
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <returns>Dados do usuário</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<object> GetUser(Guid id)
    {
        return NotFound();
    }
}

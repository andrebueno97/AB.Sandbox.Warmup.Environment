namespace UserManagementAPI.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Application.Commands;
using UserManagementAPI.Application.Queries;

[ApiController]
[Route("api/[controller]")]
public class UsersController(
    CreateUserCommandHandler createUserCommandHandler,
    UpdateUserCommandHandler updateUserCommandHandler,
    DeleteUserCommandHandler deleteUserCommandHandler,
    GetAllUsersQueryHandler getAllUsersQueryHandler,
    GetUserQueryHandler getUserQueryHandler) : ControllerBase
{
    private readonly CreateUserCommandHandler _createUserCommandHandler = createUserCommandHandler;
    private readonly UpdateUserCommandHandler _updateUserCommandHandler = updateUserCommandHandler;
    private readonly DeleteUserCommandHandler _deleteUserCommandHandler = deleteUserCommandHandler;
    private readonly GetAllUsersQueryHandler _getAllUsersQueryHandler = getAllUsersQueryHandler;
    private readonly GetUserQueryHandler _getUserQueryHandler = getUserQueryHandler;

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
    /// Atualiza parcialmente os dados de um usuário.
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <param name="command">Campos do usuário a serem atualizados (Name, Username, Password)</param>
    /// <returns>Dados atualizados do usuário</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> PatchUser(Guid id, [FromBody] UpdateUserCommand command)
    {
        var user = await _updateUserCommandHandler.HandleAsync(id, command);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(new { user.Id, user.Name, user.Username, user.CreatedAt, user.UpdatedAt });
    }

    /// <summary>
    /// Remove um usuário do sistema.
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <returns>Sem conteúdo quando o usuário é removido</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var deleted = await _deleteUserCommandHandler.HandleAsync(new DeleteUserCommand(id));
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Obtém um usuário específico.
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <returns>Dados do usuário</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetUser(Guid id)
    {
        var user = await _getUserQueryHandler.HandleAsync(new GetUserQuery(id));
        if (user is null)
        {
            return NotFound();
        }

        return Ok(new { user.Id, user.Name, user.Username, user.CreatedAt, user.UpdatedAt });
    }
}

namespace UserManagementAPI.Infrastructure.Adapters;

using Dapper;
using UserManagementAPI.Domain.Entities;
using UserManagementAPI.Domain.Ports;
using UserManagementAPI.Infrastructure.Ports;

public class UserRepository(IDbConnectionFactory dbConnectionFactory) : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT id, name, username, password_hash, created_at, updated_at FROM users WHERE id = @Id";
        
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        const string query = "SELECT id, name, username, password_hash, created_at, updated_at FROM users WHERE username = @Username";
        
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string query = "SELECT id, name, username, password_hash, created_at, updated_at FROM users ORDER BY created_at DESC";
        
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<User>(query);
    }

    public async Task<Guid> CreateAsync(User user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        const string query = @"
            INSERT INTO users (id, name, username, password_hash, created_at, updated_at)
            VALUES (@Id, @Name, @Username, @PasswordHash, @CreatedAt, @UpdatedAt)";

        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(query, user);
        
        return user.Id;
    }

    public async Task UpdateAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;

        const string query = @"
            UPDATE users 
            SET name = @Name, username = @Username, password_hash = @PasswordHash, updated_at = @UpdatedAt
            WHERE id = @Id";

        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(query, user);
    }

    public async Task DeleteAsync(Guid id)
    {
        const string query = "DELETE FROM users WHERE id = @Id";
        
        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}

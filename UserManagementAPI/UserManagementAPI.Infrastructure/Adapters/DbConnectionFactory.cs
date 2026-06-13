namespace UserManagementAPI.Infrastructure.Adapters;

using System.Data;
using Npgsql;
using UserManagementAPI.Infrastructure.Ports;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}

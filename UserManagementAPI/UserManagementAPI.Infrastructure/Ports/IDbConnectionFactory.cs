namespace UserManagementAPI.Infrastructure.Ports;

using System.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

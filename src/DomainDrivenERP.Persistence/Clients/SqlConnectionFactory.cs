using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DomainDrivenERP.Persistence.Clients;

internal class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection SqlConnection()
    {
        string? connectionString = _configuration.GetConnectionString("SqlServer");
        var connection = new SqlConnection(connectionString);
        return connection;
    }
}

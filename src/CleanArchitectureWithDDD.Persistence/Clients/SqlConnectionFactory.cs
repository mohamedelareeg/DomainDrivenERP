using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureWithDDD.Persistence.Clients;

internal class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection SqlConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("Database"));
    }
}

using Microsoft.Data.SqlClient;

namespace CleanArchitectureWithDDD.Persistence.Clients;

public interface ISqlConnectionFactory
{
    SqlConnection SqlConnection();
}

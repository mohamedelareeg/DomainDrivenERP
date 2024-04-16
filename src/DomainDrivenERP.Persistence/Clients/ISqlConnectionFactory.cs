using Microsoft.Data.SqlClient;

namespace DomainDrivenERP.Persistence.Clients;

public interface ISqlConnectionFactory
{
    SqlConnection SqlConnection();
}

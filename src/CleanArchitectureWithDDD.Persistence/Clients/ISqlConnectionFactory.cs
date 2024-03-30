using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence.Clients
{
    public interface ISqlConnectionFactory
    {
        SqlConnection SqlConnection();
    }
}

using Microsoft.Data.SqlClient;
using System.Data;

namespace Hometask.OrderManager.Data.ADO;

public class DbConnectionFactory(string connectionString)
{
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(connectionString);

        connection.Open();

        return connection;
    }
}

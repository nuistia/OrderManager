using Hometask.OrderManager.Data.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Hometask.OrderManager.Data.ADO.Repositories;

public class OrderRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public OrderRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<Order> GetOrdersLastYearWithSqlDataReader()
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT OrdId, OrdDatetime, OrdAn FROM Orders WHERE OrdDatetime >= @date";
        command.Parameters.Add(new SqlParameter("@date", DateTime.Now.AddYears(-1)));

        using var reader = command.ExecuteReader();

        var orders = new List<Order>();

        while (reader.Read())
        {
            var order = new Order
            {
                OrdId = reader.GetGuid(reader.GetOrdinal("OrdId")),
                OrdDatetime = reader.IsDBNull(reader.GetOrdinal("OrdDatetime"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("OrdDatetime")),
                OrdAn = reader.IsDBNull(reader.GetOrdinal("OrdAn"))
                    ? null
                    : reader.GetGuid(reader.GetOrdinal("OrdAn"))
            };

            orders.Add(order);
        }

        return orders;
    }

    public IEnumerable<Order> GetOrdersLastYearWithSqlDataAdapter()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Orders WHERE OrderDate >= @date";
        command.Parameters.Add(new SqlParameter("@date", DateTime.Now.AddYears(-1)));

        using var adapter = new SqlDataAdapter((SqlCommand)command);

        var dataSet = new DataSet();
        adapter.Fill(dataSet);

        var table = dataSet.Tables[0];

        var orders = new List<Order>();

        foreach (DataRow row in table.Rows)
        {
            var order = new Order
            {
                OrdId = (Guid)row["ord_id"],
                OrdDatetime = (DateTime)row["ord_datetime"],
                OrdAn = (Guid)row["ord_an"]
            };

            orders.Add(order);
        }

        return orders;
    }
}

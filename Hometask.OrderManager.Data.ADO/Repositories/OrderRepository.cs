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

    public List<Order> GetOrdersLastYear()
    {
        return (List<Order>)GetOrdersLastYearWithSqlDataReader();
    }

    public List<Order> GetOrders()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT OrdId, OrdDatetime, OrdAn FROM Orders";

        using var reader = command.ExecuteReader();

        var orders = new List<Order>();

        while (reader.Read())
        {
            orders.Add(new Order
            {
                OrdId = reader.GetGuid(reader.GetOrdinal("OrdId")),
                OrdDatetime = reader.IsDBNull(reader.GetOrdinal("OrdDatetime"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("OrdDatetime")),
                OrdAn = reader.IsDBNull(reader.GetOrdinal("OrdAn"))
                    ? null
                    : reader.GetGuid(reader.GetOrdinal("OrdAn"))
            });
        }

        return orders;
    }

    public Order? GetOrderById(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT OrdId, OrdDatetime, OrdAn FROM Orders WHERE OrdId = @id";
        command.Parameters.Add(new SqlParameter("@id", id));

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Order
            {
                OrdId = reader.GetGuid(reader.GetOrdinal("OrdId")),
                OrdDatetime = reader.IsDBNull(reader.GetOrdinal("OrdDatetime"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("OrdDatetime")),
                OrdAn = reader.IsDBNull(reader.GetOrdinal("OrdAn"))
                    ? null
                    : reader.GetGuid(reader.GetOrdinal("OrdAn"))
            };
        }

        return null;
    }

    public void AddOrder(Order order)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Orders (OrdId, OrdDatetime, OrdAn) 
            VALUES (@id, @datetime, @ordan)";

        command.Parameters.Add(new SqlParameter("@id", order.OrdId));
        command.Parameters.Add(new SqlParameter("@datetime", (object?)order.OrdDatetime ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@ordan", (object?)order.OrdAn ?? DBNull.Value));

        command.ExecuteNonQuery();
    }

    public void UpdateOrder(Order order)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Orders 
            SET OrdDatetime = @datetime, OrdAn = @ordan 
            WHERE OrdId = @id";

        command.Parameters.Add(new SqlParameter("@datetime", (object?)order.OrdDatetime ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@ordan", (object?)order.OrdAn ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@id", order.OrdId));

        command.ExecuteNonQuery();
    }

    public void DeleteOrder(Order order)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Orders WHERE OrdId = @id";
        command.Parameters.Add(new SqlParameter("@id", order.OrdId));

        command.ExecuteNonQuery();
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

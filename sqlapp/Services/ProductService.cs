using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "kprsqlappserver.database.windows.net";
        private static string db_user = "krisprematarov";
        private static string db_password = "KRISTIJANazure1!";
        private static string db_database = "sqlappdb";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;

            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection sqlConnection = GetConnection();

            List<Product> _productList = new List<Product>();

            string cmdText = "SELECT ProductID, ProductName, Quantity FROM Products";

            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(cmdText, sqlConnection);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _productList.Add(product);
                }

                sqlConnection.Close();
            }

            return _productList;
        }
    }
}

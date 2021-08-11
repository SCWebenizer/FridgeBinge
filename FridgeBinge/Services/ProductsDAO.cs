using FridgeBinge.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeBinge.Services
{
    public class ProductsDAO : IProductDataService
    {
        private readonly string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=TEst;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Create
        public int Insert(ProductModel product)
        {
            int inserted = -1;

            string sqlStatement = "INSERT INTO dbo.Product (name, price, description) VALUES (@name, @price, @description)";
            //string idSqlStatement = "SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            string idSqlStatement = "SELECT IDENT_CURRENT ('dbo.Product') AS Current_Identity; ";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand cmd = new(sqlStatement, connection);
                SqlCommand idCmd = new(idSqlStatement, connection);

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Description", product.Description);

                try
                {
                    connection.Open();

                    cmd.ExecuteNonQuery();

                    inserted = Convert.ToInt32(idCmd.ExecuteScalar());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return inserted;
        }

        // Read 1 by id
        public ProductModel GetProductById(int id)
        {
            ProductModel foundProduct = null;

            string sqlStatement = "SELECT * FROM dbo.Product WHERE Id=@Id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand cmd = new(sqlStatement, connection);

                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        foundProduct = new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] };
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return foundProduct;
        }

        // Read all
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> foundProducts = new();

            string sqlStatement = "SELECT * FROM dbo.Product";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand cmd = new(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return foundProducts;
        }

        // Read n by searchTerm
        public List<ProductModel> SearchProducts(string searchTerm)
        {
            List<ProductModel> foundProducts = new();

            string sqlStatement = "Select * from dbo.Product where Name LIKE @Name";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand cmd = new(sqlStatement, connection);

                cmd.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');

                try
                {
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProducts.Add(new ProductModel { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Description = (string)reader[3] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return foundProducts;
        }

        // Update
        public int Update(ProductModel product)
        {
            int updated = -1;

            string sqlStatement = "UPDATE dbo.Product SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand cmd = new(sqlStatement, connection);

                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Description", product.Description);

                try
                {
                    connection.Open();

                    updated = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return updated;
        }

        // Delete
        public int Delete(ProductModel product)
        {
            int deleted = -1;

            string sqlStatement = "DELETE FROM dbo.Product WHERE Id=@Id";

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand cmd = new(sqlStatement, connection);

                cmd.Parameters.AddWithValue("@Id", product.Id);

                try
                {
                    connection.Open();

                    deleted = cmd.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return deleted;
        }
    }
}

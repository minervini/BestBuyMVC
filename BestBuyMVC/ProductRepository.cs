using System.Collections.Generic;
using BestBuyMVC.Models;
using MySql.Data.MySqlClient;

namespace BestBuyMVC
{
    public class ProductRepository
    {
        private static string ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ProductID, Name, Price FROM products;";

            using (conn)
            {
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();
                List<Product> allProducts = new List<Product>();
                while (reader.Read())
                {
                    Product currentProduct = new Product();
                    currentProduct.ProductID = reader.GetInt32("ProductID");
                    currentProduct.Name = reader.GetString("Name");
                    currentProduct.Price = reader.GetDecimal("Price");

                    allProducts.Add(currentProduct);
                }
                return allProducts;
            }
        }

        public Product GetProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * From Products WHERE ProductID = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                var product = new Product();

                while (reader.Read() == true)
                {
                    product.ProductID = reader.GetInt32("ProductID");
                    product.Name = reader.GetString("Name");
                    product.Price = reader.GetDecimal("Price");
                    product.OnSale = reader.GetInt32("OnSale");
                  //product.StockLevel = reader.GetString("StockLevel");
                    product.CategoryID = reader.GetInt32("CategoryID");

                    if (reader.IsDBNull(reader.GetOrdinal("StockLevel")))
                    {
                        product.StockLevel = null;
                    }
                    else
                    {
                        product.StockLevel = reader.GetString("StockLevel");
                    }
                }
                return product;
            }
        }

        public void UpdateProduct(Product productToUpdate)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE products Set Name =@name, Price = @price WHERE ProductID = @ID";

            cmd.Parameters.AddWithValue("name", productToUpdate.Name);
            cmd.Parameters.AddWithValue("price", productToUpdate.Price);
            cmd.Parameters.AddWithValue("ID", productToUpdate.ProductID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertProduct(Product productToInsert)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "INSERT INTO products (Name, Price, CategoryID) Values (@name, @price, @categoryID)";

            cmd.Parameters.AddWithValue("name", productToInsert.Name);
            cmd.Parameters.AddWithValue("price", productToInsert.Price);
            cmd.Parameters.AddWithValue("categoryID", productToInsert.CategoryID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Product AssignCategories()
        {
            var catRepo = new CategoryRepository();

            var catList = catRepo.GetCategories();

            Product product = new Product();
            product.Categories = catList;

            return product;
        }

        public void DeleteFromSales(int productID)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete FROM sales WHERE ProductID = @id;";
            cmd.Parameters.AddWithValue("id", productID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFromReviews(int productID)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete FROM reviews WHERE ProductID = @id;";
            cmd.Parameters.AddWithValue("id", productID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete FROM products WHERE ProductID = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFromAllTables(int productID)
        {
            DeleteFromSales(productID);
            DeleteFromReviews(productID);
            DeleteProduct(productID);
        }
    }
}

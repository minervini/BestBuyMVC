using System;
using System.Collections.Generic;
using BestBuyMVC.Models;
using MySql.Data.MySqlClient;

namespace BestBuyMVC
{
    public class ProductRepository
    {
        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ProductID, Name, Price FROM products;";

            using (conn)
            {
                conn.Open();
                //cmd.ExecuteNonQuery();

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
    }
}

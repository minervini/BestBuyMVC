using System.Collections.Generic;
using BestBuyMVC.Models;
using MySql.Data.MySqlClient;

namespace BestBuyMVC
{
    public class CategoryRepository
    {
        private static string ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public List<Category> GetCategories()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM Categories;";

            using (conn)
            {
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                var allCategories = new List<Category>();

                while (reader.Read())
                {
                    var currentCategory = new Category();
                    currentCategory.CategoryID = reader.GetInt32("CategoryID");
                    currentCategory.Name = reader.GetString("Name");
                    allCategories.Add(currentCategory);
                }
                return allCategories;
            }
        }
    }
}

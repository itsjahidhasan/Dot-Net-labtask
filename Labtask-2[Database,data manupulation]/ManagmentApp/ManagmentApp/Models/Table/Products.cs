using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ManagmentApp.Models.Entity;

namespace ManagmentApp.Models.Table
{
    public class Products
    {
        SqlConnection conn;
        public Products(SqlConnection conn)
        {
            this.conn = conn;
        }
        public void Add(Product p)
        {
            string query = String.Format("insert into Products values ('{0}','{1}','{2}','{3}')", p.Name, p.Price, p.Quantity, p.Description);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int r = cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Product GetOneProduct(int id)
        {
            string query = String.Format("select * from Products where Id={0}", id);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Product selectedProduct = new Product();
            selectedProduct.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            selectedProduct.Name = reader.GetString(reader.GetOrdinal("Name"));
            selectedProduct.Price = reader.GetInt32(reader.GetOrdinal("Price"));
            selectedProduct.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
            selectedProduct.Description = reader.GetString(reader.GetOrdinal("Description"));

            conn.Close();
            return selectedProduct;
        }
        public List<Product> GetAll()
        {
            string query = "select * from Products";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Product> Products = new List<Product>();
            while (reader.Read())
            {
                Product p = new Product()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Price = reader.GetInt32(reader.GetOrdinal("Price")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                };
                Products.Add(p);
            }
            conn.Close();
            return Products;
        }
        public void Update(Product p)
        {
            string query = String.Format("update Products set Name='{0}',Price='{1}',Quantity='{2}',Description='{3}' where Id ='{4}'", p.Name, p.Price, p.Quantity,
                p.Description, p.Id);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int r = cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Product DeleteProduct(int id)
        {
            string query = String.Format("delete from Products where Id={0}", id);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int r = cmd.ExecuteNonQuery();

            conn.Close();
            return null;
        }
    }
}
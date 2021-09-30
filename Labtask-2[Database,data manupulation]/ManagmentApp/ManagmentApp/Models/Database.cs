using ManagmentApp.Models.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ManagmentApp.Models
{
    public class Database
    {
        public Products Products { get; set; }
        public Database()
        {
            string connString = @"Server=LAPTOP-CTA7J4T7\SQLEXPRESS;Database=DotNetLabwork;Integrated Security=true";
            SqlConnection conn = new SqlConnection(connString);
            Products = new Products(conn);
            
        }
    }
}
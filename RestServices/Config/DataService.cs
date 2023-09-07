﻿using System.Data;
using System.Data.SqlClient;

namespace REST.Config
{
    public class DataService
    {
        IConfiguration config;
        SqlConnection con;
        SqlCommand? cmd;
        public DataService(IConfiguration _config) 
        {
            this.config = _config;
            con = new SqlConnection();
            con.ConnectionString = config["ConnectionStrings:cstr"];
        }
        
        public string? FindUser(AppUserCredentalsModel model)
        {
            string? role = null;
            cmd = new SqlCommand();
            cmd.Connection = con;
            
            cmd.CommandText = "select role from AppUsers where username=@user and password=@pwd";
            
            cmd.Parameters.AddWithValue("@user", model.UserName);
            cmd.Parameters.AddWithValue("@pwd", model.Password);
            con.Open();
            var result=cmd.ExecuteScalar();
            if (result != null) { role = result.ToString(); }
            con.Close();
            return role;
        }
    }
}

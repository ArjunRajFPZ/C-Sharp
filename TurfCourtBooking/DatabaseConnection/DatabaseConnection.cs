using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using TurfCourtBooking.Models;

namespace TurfCourtBooking.DatabaseConnection
{
    public class DatabaseConnection
    {
        string connString = ConfigurationManager.ConnectionStrings["databaseconnection"].ToString();

        public bool UserDataAdd(RegistrationModel user)
        {
            #region User Add
            int returnvalue = 0;
            Guid Id = Guid.NewGuid();
            string usertype = "user";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserAdd";
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Dateofbirth", user.Dateofbirth);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Usertype", usertype);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
            #endregion
        }
        public List<RegistrationModel> UserDataView()
        {
            #region User View
            List<RegistrationModel> userList = new List<RegistrationModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Userview";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    userList.Add(new RegistrationModel
                    {
                        Name = dataRow["Name"].ToString(),
                        Dateofbirth = Convert.ToDateTime(dataRow["Dateofbirth"]).Date,
                        Phone = dataRow["Phone"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Password = dataRow["Password"].ToString(),
                        Usertype = dataRow["Usertype"].ToString(),
                    });
                }
            }
            return userList;
            #endregion
        }
        public RegistrationModel LoginCheck(string email, string password)
        {
            #region Login
            RegistrationModel check = new RegistrationModel();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Login";
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    check.Name = reader["Name"].ToString();
                    check.Dateofbirth = Convert.ToDateTime(reader["Dateofbirth"]).Date;
                    check.Phone = reader["Phone"].ToString();
                    check.Address = reader["Address"].ToString();
                    check.Email = reader["Email"].ToString();
                    check.Usertype = reader["Usertype"].ToString();
                }
                connection.Close();
            }
            return check;
            #endregion
        }
    }
}
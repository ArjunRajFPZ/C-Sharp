using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using WebAPILogBook.Models;

namespace WebAPILogBook.Controllers
{
    public class APIController : ApiController
    {
        string connString = ConfigurationManager.ConnectionStrings["databaseconnection"].ToString();

        #region POST User values
        [HttpPost]
        public string InsertUserData(UserModel userRegisteration)
        {
            string returnMessage = "";
            if (userRegisteration != null)
            {
                int returnValue = 0;
                string usertype = "User";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UserAdd";
                    command.Parameters.AddWithValue("@Name", userRegisteration.Name);
                    command.Parameters.AddWithValue("@DateOfBirth", userRegisteration.DateOfBirth);
                    command.Parameters.AddWithValue("@Email", userRegisteration.Email);
                    command.Parameters.AddWithValue("@Password", userRegisteration.Password);
                    command.Parameters.AddWithValue("@UserType", usertype);

                    connection.Open();
                    returnValue = command.ExecuteNonQuery();
                    connection.Close();

                    if (returnValue > 0)
                    {
                        returnMessage = "Data Inserted Successfully.";
                    }
                    else
                    {
                        returnMessage = "Data Insertion Failed!";
                    }
                }
            }
            return returnMessage;
        }
        #endregion

        #region GET User values
        [HttpGet]
        public List<UserModel> RetriveUserData()
        {
            List<UserModel> userList = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UserView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    userList.Add(new UserModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Name = dataRow["Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dataRow["DateOfBirth"]).Date,
                        Email = dataRow["Email"].ToString(),
                        Password = dataRow["Password"].ToString(),
                        UserType = dataRow["UserType"].ToString(),
                    });
                }
            }
            return userList;
        }
        #endregion        
    }
}

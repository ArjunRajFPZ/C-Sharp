using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPILogBook.Models;

namespace WebAPILogBook.Controllers
{
    public class LoginController : ApiController
    {
        string connString = ConfigurationManager.ConnectionStrings["databaseconnection"].ToString();

        #region POST Login Check
        [HttpPost]
        public HttpResponseMessage LoginCheck(UserModel userModel)
        {
            UserModel check = new UserModel();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "LoginCheck";
                command.Parameters.AddWithValue("@Email", userModel.Email);
                command.Parameters.AddWithValue("@Password", userModel.Password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    check.Email = reader["Email"].ToString();
                    check.UserType = reader["UserType"].ToString();
                }
                connection.Close();
            }
            if(check.Email != null) 
            {
                var data = ValidLogin(check.Email, check.UserType);
                return data;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Valid Login
        [HttpGet]
        public HttpResponseMessage ValidLogin(string Email, string UserType)
        {
            if (Email != "" && UserType != "")
            {
                return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GetToken(Email, UserType));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, message: "Invalid User");
            }
        } 
        #endregion
    }
}

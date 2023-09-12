using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using WebAPILogBook.Models;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace WebAPILogBook.Controllers
{
    public class LogAPIController : ApiController
    {
        string connString = ConfigurationManager.ConnectionStrings["databaseconnection"].ToString();

        #region POST Log values
        [HttpPost]
        public string WorkLogAdd(WorkLogModel workLog)
        {
            string returnMessage = "";
            if (workLog != null)
            {
                int returnValue = 0;
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "WorkLogAdd";
                    command.Parameters.AddWithValue("@UserName", workLog.UserName);
                    command.Parameters.AddWithValue("@DateOfLog", workLog.DateOfLog);
                    command.Parameters.AddWithValue("@StartTime", workLog.StartTime);
                    command.Parameters.AddWithValue("@EndTime", workLog.EndTime);

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

        #region GET Log values
        [HttpGet]
        public List<WorkLogModel> WorkLogView()
        {
            List<WorkLogModel> userList = new List<WorkLogModel>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "WorkLogView";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    userList.Add(new WorkLogModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        UserName = dataRow["UserName"].ToString(),
                        DateOfLog = Convert.ToDateTime(dataRow["DateOfLog"]).Date,
                        StartTime = dataRow["StartTime"].ToString(),
                        EndTime = dataRow["EndTime"].ToString(),
                    });
                }
            }
            return userList;
        }
        #endregion
    }
}

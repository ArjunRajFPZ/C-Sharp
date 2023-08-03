using CRUDWebApplication.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Claims;

namespace CRUDWebApplication.Areas.Identity.Data
{
    public class SPTaskbookConnection
    {
        static string connectionString = @"Server=DESKTOP-15TU749\MSSQLSERVER03;Database=TaskbookManagementSystem;Trusted_Connection=True;TrustServerCertificate=True";
 
        public List<SPTaskbookModel> TaskbookDataView(string userEmail)
        {
            #region Taskbook View
            List<SPTaskbookModel> taskbookList = new List<SPTaskbookModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spTaskbookEmailView";
                command.Parameters.AddWithValue("@Email", userEmail);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    taskbookList.Add(new SPTaskbookModel
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Name = dataRow["Name"].ToString(),
                        Assignedfrom = dataRow["Assignedfrom"].ToString(),
                        Assignedto = dataRow["Assignedto"].ToString(),
                        Assigneddate = Convert.ToDateTime(dataRow["Assigneddate"]).Date,
                        Status = dataRow["Status"].ToString(),
                        Email = dataRow["Email"].ToString(),
                    });
                }
            }
            return taskbookList;
            #endregion
        }
        public bool TaskbookDataAdd(SPTaskbookModel taskbook)
        {
            #region Taskbook Add
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spTaskbookAdd";
                command.Parameters.AddWithValue("@Name", taskbook.Name);
                command.Parameters.AddWithValue("@Assignedfrom", taskbook.Assignedfrom);
                command.Parameters.AddWithValue("@Assignedto", taskbook.Assignedto);
                command.Parameters.AddWithValue("@Assigneddate", taskbook.Assigneddate);
                command.Parameters.AddWithValue("@Status", taskbook.Status);
                command.Parameters.AddWithValue("@Email", taskbook.Email);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
            #endregion
        }
        public SPTaskbookModel TaskbookDataEdit(int id)
        {
            #region Taskbook Edit
            SPTaskbookModel taskbook = new SPTaskbookModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spTaskbookIdView";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    taskbook.Id = Convert.ToInt32(reader["Id"]);
                    taskbook.Name = reader["Name"].ToString();
                    taskbook.Assignedfrom = reader["Assignedfrom"].ToString();
                    taskbook.Assignedto = reader["Assignedto"].ToString();
                    taskbook.Assigneddate = Convert.ToDateTime(reader["Assigneddate"]).Date;
                    taskbook.Status = reader["Status"].ToString();
                    taskbook.Email = reader["Email"].ToString();
                }
                connection.Close();
            }
            return taskbook;
            #endregion
        }
        public bool TaskbookDataUpdate(SPTaskbookModel taskbook)
        {
            #region Taskbook Update
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spTaskbookIdUpdate";
                command.Parameters.AddWithValue("@Id", taskbook.Id);
                command.Parameters.AddWithValue("@Name", taskbook.Name);
                command.Parameters.AddWithValue("@Assignedfrom", taskbook.Assignedfrom);
                command.Parameters.AddWithValue("@Assignedto", taskbook.Assignedto);
                command.Parameters.AddWithValue("@Assigneddate", taskbook.Assigneddate);
                command.Parameters.AddWithValue("@Status", taskbook.Status);
                command.Parameters.AddWithValue("@Email", taskbook.Email);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
            #endregion
        }
        public bool TaskbookDataDelete(int id)
        {
            #region Taskbook Delete
            int returnvalue = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spTaskbookIdDelete";
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                returnvalue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnvalue > 0 ? true : false;
            #endregion
        }
    }
}

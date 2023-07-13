using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace SqliteTaskbookConsoleApp
{
    internal class DatabaseOperationAndConnection
    {
        SQLiteConnection sqliteConnection;
        SQLiteCommand command;
        SQLiteDataReader dataread;
        public void CreateDatabaseAndTable()
        {
            #region Database create and connect
            if (!File.Exists("TaskBookDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("TaskBookDatabase.sqlite");
                string sql = @"CREATE TABLE TaskRecords(ID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,Assigned_from TEXT NOT NULL,Assigned_to TEXT NOT NULL,
                            Assigned_date TEXT NOT NULL,Task_duration TEXT NOT NULL,Status TEXT NOT NULL);";
                sqliteConnection = new SQLiteConnection("Data Source=TaskBookDatabase.sqlite;Version=3;");
                sqliteConnection.Open();
                command = new SQLiteCommand(sql, sqliteConnection);
                command.ExecuteNonQuery();
                sqliteConnection.Close();
            }
            else
            {
                sqliteConnection = new SQLiteConnection("Data Source=TaskBookDatabase.sqlite;Version=3;");
            } 
            #endregion
        }
        public void InsertIntoTable()
        {
            #region UI for data insertion
            string name = "", assignedfrom = "", assignedto = "", assigneddate = "", taskduration = "", status = "";
            Console.WriteLine("\n-------------------------");
            Console.Write("Enter the task name     : ");
            name = Console.ReadLine();
            Console.Write("Task assigned from      : ");
            assignedfrom = Console.ReadLine();
            Console.Write("Task assigned to        : ");
            assignedto = Console.ReadLine();
            Console.Write("Task assigned date      : ");
            assigneddate = Console.ReadLine();
            Console.Write("Enter the task duration : ");
            taskduration = Console.ReadLine();
            Console.Write("Task status             : ");
            status = Console.ReadLine();
            #endregion  

            #region Data insertion
            command = new SQLiteCommand();
            try
            {
                sqliteConnection.Open();
                command.Connection = sqliteConnection;
                command.CommandText = "insert into TaskRecords(Name,Assigned_from,Assigned_to,Assigned_date,Task_duration,Status) " +
                    "values ('" + name + "','" + assignedfrom + "','" + assignedto + "','" + assigneddate + "','" + taskduration + "','" + status + "')";
                command.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            sqliteConnection.Close();
            #endregion
        }
        public void DisplayDataFromTable()
        {
            #region Data display
            int counter = 0;
            command = new SQLiteCommand("Select * From TaskRecords", sqliteConnection);
            sqliteConnection.Open();
            dataread = command.ExecuteReader();
            Console.WriteLine("\n-------------------------\n\n\tTaskbook Records\n");
            while (dataread.Read())
            {
                counter++;
                Console.WriteLine($"ID            : {dataread[0]}");
                Console.WriteLine($"Name          : {dataread[1]}");
                Console.WriteLine($"Assigned from : {dataread[2]}");
                Console.WriteLine($"Assigned to   : {dataread[3]}");
                Console.WriteLine($"Assigned date : {dataread[4]}");
                Console.WriteLine($"Task duration : {dataread[5]}");
                Console.WriteLine($"Status        : {dataread[6]}\n");
            }
            sqliteConnection.Close(); 
            #endregion
        }
        public void DeleteFromTable()
        {
            #region Take delete id UI
            int deleteid = 0, counter = 0, userid = 0;
            Console.Write("\nEnter the id to delete : ");
            deleteid = Convert.ToInt32(Console.ReadLine());
            #endregion

            #region Check id and delete data
            command = new SQLiteCommand("Select ID From TaskRecords", sqliteConnection);
            sqliteConnection.Open();
            dataread = command.ExecuteReader();
            while (dataread.Read())
            {
                counter++;
                userid = Convert.ToInt32(dataread[0]);
            }
            if (userid == deleteid)
            {
                command = sqliteConnection.CreateCommand();
                command.CommandText = String.Format("Delete FROM TaskRecords WHERE ID={0}", deleteid);
                command.ExecuteNonQuery();
                Console.WriteLine($"\nId {deleteid} deleted From table.");
            }
            else
            {
                Console.WriteLine($"\nId {deleteid} not found in table !");
            }
            sqliteConnection.Close();
            #endregion
        }
        public void UpadteToTable()
        {
            #region Check and update data
            int updateid = 0, counter = 0, userid = 0;
            string name = "", assignedfrom = "", assignedto = "", assigneddate = "", taskduration = "", status = "";
            Console.Write("\nEnter the id to update : ");
            updateid = Convert.ToInt32(Console.ReadLine());
            sqliteConnection.Open();
            command = new SQLiteCommand("Select ID From TaskRecords", sqliteConnection);
            dataread = command.ExecuteReader();
            while (dataread.Read())
            {
                counter++;
                userid = Convert.ToInt32(dataread[0]);
            }

            #region UI and update
            if (userid == updateid)
            {
                Console.WriteLine("\n-------------------------");
                Console.Write("Enter the task name     : ");
                name = Console.ReadLine();
                Console.Write("Task assigned from      : ");
                assignedfrom = Console.ReadLine();
                Console.Write("Task assigned to        : ");
                assignedto = Console.ReadLine();
                Console.Write("Task assigned date      : ");
                assigneddate = Console.ReadLine();
                Console.Write("Enter the task duration : ");
                taskduration = Console.ReadLine();
                Console.Write("Task status             : ");
                status = Console.ReadLine();
                command = sqliteConnection.CreateCommand();
                command.CommandText = String.Format
                                    ("UPDATE TaskRecords SET Name='" + name + "',Assigned_from='" + assignedfrom + "',Assigned_to='" + assignedto + "'," +
                                    "Assigned_date='" + assigneddate + "',Task_duration='" + taskduration + "',Status='" + status + "' WHERE ID={0}", updateid);
                command.ExecuteNonQuery();
                Console.WriteLine($"\nData updated to id : {updateid}.");
            }
            else
            {
                Console.WriteLine($"\nNo data with with id {updateid} found !");
            } 
            #endregion
            sqliteConnection.Close();
            #endregion
        }
    }
}

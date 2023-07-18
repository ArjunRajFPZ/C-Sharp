using System;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

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
        public void InsertValueValidation()
        {
            #region Inserted value validation
            string name, assignedfrom, assignedto, assigneddate, taskduration, status;
            Console.WriteLine("\n-------------------------");
            Console.Write("Enter the task name              : ");
            name = Console.ReadLine();
            if (Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
            {
                Console.Write("Task assigned from               : ");
                assignedfrom = Console.ReadLine();
                if (Regex.IsMatch(assignedfrom, @"^[a-zA-Z ]+$"))
                {
                    Console.Write("Task assigned to                 : ");
                    assignedto = Console.ReadLine();
                    if (Regex.IsMatch(assignedto, @"^[a-zA-Z ]+$"))
                    {
                        Console.Write("Enter date in (dd/mm/yyyy) format\n");
                        Console.Write("Task assigned date               : ");
                        assigneddate = Console.ReadLine();
                        if (Regex.IsMatch(assigneddate, @"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$"))
                        {
                            Console.Write("Enter the task duration in hours : ");
                            taskduration = Console.ReadLine();
                            if (Regex.IsMatch(taskduration, @"^[0-9.]+$"))
                            {
                                Console.Write("Task status                      : ");
                                status = Console.ReadLine();
                                if (Regex.IsMatch(status, @"^[a-zA-Z ]+$"))
                                {
                                    InsertData(name, assignedfrom, assignedto, assigneddate, taskduration, status);
                                }
                                else
                                {
                                    Console.WriteLine("\nStatus value is empty or has numbers in it ! please try again.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nTask duration value is empty or has string in it ! please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nAssigned date value is empty or is not in correct format ! please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nAssigned to value is empty or has numbers in it ! please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("\nAssigned from value is empty or has numbers in it ! please try again.");
                }
            }
            else
            {
                Console.WriteLine("\nName value is empty or has numbers in it ! please try again.");
            }
            #endregion
        }
        public void InsertData(string name, string assignedfrom, string assignedto, string assigneddate, string taskduration, string status)
        {
            #region Data insertion
            try
            {
                command = new SQLiteCommand();
                sqliteConnection.Open();
                command.Connection = sqliteConnection;
                command.CommandText = "insert into TaskRecords(Name,Assigned_from,Assigned_to,Assigned_date,Task_duration,Status) " +
                    "values ('" + name + "','" + assignedfrom + "','" + assignedto + "','" + assigneddate + "','" + taskduration + "','" + status + "')";
                command.ExecuteNonQuery();
                sqliteConnection.Close();
                Console.WriteLine("\nData inserted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            #endregion
        }
        public void DisplayDataFromTable()
        {
            #region Data display
            sqliteConnection.Open();
            command = new SQLiteCommand("Select * From TaskRecords", sqliteConnection);
            dataread = command.ExecuteReader();
            Console.WriteLine("\n-------------------------\n\n\tTaskbook Records\n");
            while (dataread.Read())
            {
                Console.WriteLine($"ID            : {dataread[0]}");
                Console.WriteLine($"Name          : {dataread[1]}");
                Console.WriteLine($"Assigned from : {dataread[2]}");
                Console.WriteLine($"Assigned to   : {dataread[3]}");
                Console.WriteLine($"Assigned date from: {dataread[4]}");
                Console.WriteLine($"Task duration : {dataread[5]}");
                Console.WriteLine($"Status        : {dataread[6]}\n");
            }
            sqliteConnection.Close();
            #endregion
        }
        public void DeleteFromTable()
        {
            #region Take delete id UI
            int deleteid;
            Console.Write("\nEnter the id to delete : ");
            deleteid = Convert.ToInt32(Console.ReadLine());
            #endregion

            #region Check id and delete data
            command = new SQLiteCommand("Select ID From TaskRecords where ID ='" + deleteid + "'", sqliteConnection);
            sqliteConnection.Open();
            dataread = command.ExecuteReader();
            if (dataread.Read() != false)
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
        public void UpadteValueValidation()
        {
            #region Check id and data validation
            int updateid;
            Console.Write("\nEnter the id to update : ");
            updateid = Convert.ToInt32(Console.ReadLine());
            sqliteConnection.Open();
            command = new SQLiteCommand("Select ID From TaskRecords Where ID ='" + updateid + "'", sqliteConnection);
            dataread = command.ExecuteReader();
            if (dataread.Read() != false)
            {
                UpdateIntoTable(updateid);
            }
            else
            {
                Console.Write($"\nNo data for the id {updateid} found !\n");
            }
            sqliteConnection.Close();
            #endregion
        }
        public void UpdateIntoTable(int updateid)
        {
            #region Data validation and update
            string name, assignedfrom, assignedto, assigneddate, taskduration, status;
            Console.WriteLine("\n-------------------------");
            Console.Write("Enter the task name              : ");
            name = Console.ReadLine();
            if (Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
            {
                Console.Write("Task assigned from               : ");
                assignedfrom = Console.ReadLine();
                if (Regex.IsMatch(assignedfrom, @"^[a-zA-Z ]+$"))
                {
                    Console.Write("Task assigned to                 : ");
                    assignedto = Console.ReadLine();
                    if (Regex.IsMatch(assignedto, @"^[a-zA-Z ]+$"))
                    {
                        Console.Write("Enter date in (dd/mm/yyyy) format\n");
                        Console.Write("Task assigned date               : ");
                        assigneddate = Console.ReadLine();
                        if (Regex.IsMatch(assigneddate, @"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$"))
                        {
                            Console.Write("Enter the task duration in hours : ");
                            taskduration = Console.ReadLine();
                            if (Regex.IsMatch(taskduration, @"^[0-9.]+$"))
                            {
                                Console.Write("Task status                      : ");
                                status = Console.ReadLine();
                                if (Regex.IsMatch(status, @"^[a-zA-Z ]+$"))
                                {
                                    command = new SQLiteCommand();
                                    command = sqliteConnection.CreateCommand();
                                    command.CommandText = String.Format
                                                        ("UPDATE TaskRecords SET Name='" + name + "',Assigned_from='" + assignedfrom + "',Assigned_to='" + assignedto + "'," +
                                                        "Assigned_date='" + assigneddate + "',Task_duration='" + taskduration + "',Status='" + status + "' WHERE ID={0}", updateid);
                                    command.ExecuteNonQuery();
                                    Console.WriteLine($"\nData updated to id : {updateid}.");
                                }
                                else
                                {
                                    Console.WriteLine("\nStatus value is empty or has numbers in it ! please try again.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nTask duration value is empty or has string in it ! please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nAssigned date value is empty or is not in correct format ! please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nAssigned to value is empty or has numbers in it ! please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("\nAssigned from value is empty or has numbers in it ! please try again.");
                }
            }
            else
            {
                Console.WriteLine("\nName value is empty or has numbers in it ! please try again.");
            }
            #endregion
        }

        public void DatewiseSelection()
        {
            #region Date from user UI
            string tofinddate;
            Console.Write("\nEnter the assign date you want to retrive task details for : ");
            tofinddate = Console.ReadLine();
            #endregion

            #region Datewise select and display
            sqliteConnection.Open();
            command = new SQLiteCommand("Select * From TaskRecords Where Assigned_date = '" + tofinddate + "'", sqliteConnection);
            dataread = command.ExecuteReader();
            while (dataread.Read())
            {
                Console.WriteLine($"\nID            : {dataread[0]}");
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
    }
}

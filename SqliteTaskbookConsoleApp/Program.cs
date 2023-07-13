using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteTaskbookConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseOperationAndConnection sqliteConnection = new DatabaseOperationAndConnection();
            sqliteConnection.CreateDatabaseAndTable();

            #region UI with switch condition for Taskbook
            int choice = 0;
            Console.WriteLine("\tTaskbook");
            do
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("\nSelect option");
                Console.WriteLine("1. Insert data \n2. View data \n3. Update data \n4. Delete data \n5. Exit");
                Console.Write("\nEnter your choice : ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        sqliteConnection.InsertIntoTable();
                        break;
                    case 2:
                        sqliteConnection.DisplayDataFromTable();
                        break;
                    case 3:
                        sqliteConnection.UpadteToTable();
                        break;
                    case 4:
                        sqliteConnection.DeleteFromTable();
                        break;
                    case 5:
                        Console.WriteLine("Ready to exit");
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (choice != 5);
            #endregion
        }
    }
}
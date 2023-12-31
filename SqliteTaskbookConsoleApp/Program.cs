﻿using System;

namespace SqliteTaskbookConsoleApp
{
    internal class Program
    {
        static void ChoiceUI()
        {
            #region Choice UI
            Console.WriteLine("\n\tTaskbook");
            Console.WriteLine("-------------------------");
            Console.WriteLine("\nSelect option");
            Console.WriteLine("1. Insert data \n2. View data \n3. Update data \n4. Delete data \n5. Datewise view \n6. Exit");
            Console.Write("\nEnter your choice : ");
            #endregion
        }
        static void Main(string[] args)
        {
            DatabaseOperationAndConnection sqliteConnection = new DatabaseOperationAndConnection();
            sqliteConnection.CreateDatabaseAndTable();

            #region UI with switch condition for Taskbook
            string userInput;
            int choice;
            bool wantToExit = false;
            while (!wantToExit)
            {
                ChoiceUI();
                userInput = Console.ReadLine();
                int.TryParse(userInput, out choice);
                switch (choice)
                {
                    case 1:
                        sqliteConnection.InsertValueValidation();
                        break;
                    case 2:
                        sqliteConnection.DisplayDataFromTable();
                        break;
                    case 3:
                        sqliteConnection.UpadteValueValidation();
                        break;
                    case 4:
                        sqliteConnection.DeleteFromTable();
                        break;
                    case 5:
                        sqliteConnection.DatewiseSelection();
                        break;
                    case 6:
                        Console.WriteLine("Ready to exit");
                        wantToExit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
            #endregion
        }
    }
}
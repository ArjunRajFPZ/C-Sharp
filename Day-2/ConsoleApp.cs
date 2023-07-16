using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static string[] AddTask()
        {
            string name = "", assignfrom = "", assignto = "", status = "", duration = "", date = "";

            Console.WriteLine("\t TASK BOOK");
            Console.WriteLine("---------------------------------");
            Console.WriteLine();
            Console.WriteLine("Enter Task Name = ");
            name = Console.ReadLine();
            Console.WriteLine("Enter Task Assigned From = ");
            assignfrom = Console.ReadLine();
            Console.WriteLine("Enter Task Assigned To = ");
            assignto = Console.ReadLine();
            Console.WriteLine("Enter Task Date in DD/MM/YYYY Format ");
            date = Console.ReadLine();
            Console.WriteLine("Enter Task Duration(only enter no of hours) = ");
            duration = Console.ReadLine();
            Console.WriteLine("Enter Task Status = ");
            status = Console.ReadLine();
            Console.WriteLine();
            string[] task = { name, assignfrom, assignto, date, duration, status };
            return task;
        }        
        static void ViewTask(string[] arr)
        {
                Console.WriteLine("\t TASK BOOK DETAILS");
                Console.WriteLine("-----------------------------");
                Console.WriteLine();
                Console.WriteLine($"Task Name = {arr[0]}");
                Console.WriteLine($"Task Assigned From = {arr[1]}");
                Console.WriteLine($"Task Assigned To = {arr[2]}");
                Console.WriteLine($"Task Date = {arr[3]}");
                Console.WriteLine($"Task Duration = {arr[4]} hours");
                Console.WriteLine($"Task Status = {arr[5]}");
                Console.WriteLine();
        }
        static void EditTask(string[] arr)
        {
            int option = 0;
            string data = "";
            Console.WriteLine("Select field to change data");
            Console.WriteLine("1. Task Name \n2. Task Assigned From \n3. Task Assigned To \n4. Task Date \n5. Task Duration \n6. Task Status");
            option = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Enter New Data to change");
            data = Console.ReadLine();
            string arr[option] = data;
        }
        static void DeleteTask(string[] arr)
        {
            Array.Clear(arr, 0, arr.Length);
        }
        static void ExitTask()
        {
            Console.Write("Getting Ready to Exit.\n");
        }

        static void Main(string[] args)
        {
            int choice = 0;
            string[] arr = AddTask();
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please Select a option ");
                Console.WriteLine("1. Add Task \n2. Edit Task \n3. Delete Task \n4. Task Details \n5. Exit");
                Console.WriteLine();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddTask();
                        break;
                    case 2:
                        EditTask(arr);
                        break;
                    case 3:
                        DeleteTask(arr);
                        break;
                    case 4:
                        ViewTask(arr);
                        break;
                    case 5:
                        ExitTask();
                        break;
                    default:
                        Console.WriteLine("Invalid Input ");
                        break;
                }
            } while (choice != 5);
        }
    }
}

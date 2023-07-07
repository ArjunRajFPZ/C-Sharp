using System;

namespace Program1
{
    internal class Program4
    {
        static void Main(string[] args)
        {
            int firstnum, secondnum, choice;
            Console.WriteLine("Enter First num");
            firstnum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Second num");
            secondnum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("1. Add \n2. Substract \n3. Multiply \n4. Divide \n5. Modulus");
            Console.WriteLine("Enter Choice : ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    int sum = firstnum + secondnum;
                    Console.WriteLine($"Sum of number {firstnum} and {secondnum} is {sum}");
                    break;
                case 2:
                    int sub = firstnum - secondnum;
                    Console.WriteLine($"Substract of number {firstnum} and {secondnum} is {sub}");
                    break;
                case 3:
                    int mul = firstnum * secondnum;
                    Console.WriteLine($"Multiply of number {firstnum} and {secondnum} is {mul}");
                    break;
                case 4:
                    int div = firstnum / secondnum;
                    Console.WriteLine($"Divide of number {firstnum} and {secondnum} is {div}");
                    break;
                case 5:
                    int mod = firstnum % secondnum;
                    Console.WriteLine($"Modulus of number {firstnum} and {secondnum} is {mod}");
                    break;
                default: Console.WriteLine("Invalid Input");
                    break;
            }


        }
    }
}

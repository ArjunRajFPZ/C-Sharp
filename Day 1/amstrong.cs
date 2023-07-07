using System;

namespace Program1
{
    internal class Program5
    {
        static void Main(string[] args)
        {
            int number,temp,rem=0,rev=0;
            Console.WriteLine("Enter the number to check");
            number = Convert.ToInt32(Console.ReadLine());
            temp = number;
            while(temp >0)
            {
                rem = temp % 10;
                rev = rev + (rem * rem * rem);
                temp = temp / 10;
            }
            if(number == rev) {
                Console.WriteLine($"{number} is Amstrong");
            }
            else
            {
                Console.WriteLine($"{number} is not Amstrong");
            }
        }
    }
}

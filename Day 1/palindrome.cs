using System;

namespace Program1
{
    internal class Program2
    {
        static void Main(string[] args)
        {
            string s, revs = "";
            Console.WriteLine(" Enter string");
            s = Console.ReadLine();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                revs += s[i].ToString();
            }
            if (revs == s)
            {
                Console.WriteLine("Is Palindrome");
            }
            else
            {
                Console.WriteLine("Is Not Palindrome");
            }
        }
    }
}

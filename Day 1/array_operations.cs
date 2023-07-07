using System;
using System.Linq;

namespace Program1
{
    internal class Program6
    {
        static void Main(string[] args)
        {
            int[] arr = {10,20,30,50,40,90};
            float sum = arr.Sum();
            Console.WriteLine(sum);
            
            int count = arr.Count();
            float avg = sum/count;
            Console.Write($"{avg}\n");
            
            for (int i = 0; i < arr.Length; i++) 
            {
              Console.WriteLine($"\n {arr[i]}");
            }
            
             Array.Sort(arr);
             foreach (int i in arr)
             {
               Console.WriteLine(i);
             }
            
        }
    }
}

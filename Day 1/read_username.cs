using System;

namespace Program1
{
  internal class Program3
  {
    static void Main(string[] args)
    {
      string firstname, lastname, fullname;
      Console.WriteLine("Enter First name = ");
      firstname = Console.ReadLine();
      Console.WriteLine("Enter Last name = ");
      lastname = Console.ReadLine();
      fullname = firstname + lastname;
      Console.WriteLine("Fullname is = " + fullname);
      Console.WriteLine(fullname);
    }
  }
}

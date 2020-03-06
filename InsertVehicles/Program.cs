using System;
using System.Collections.Generic;
using System.Linq;

namespace InsertVehicles
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var acura = new List<string>();

            while (input !="stop")
            {
                acura.Add(input);
                input = Console.ReadLine();
            }
            Console.WriteLine(string.Join(" ", acura));
        }
    }
}

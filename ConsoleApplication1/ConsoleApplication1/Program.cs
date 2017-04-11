using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {



                double input = Convert.ToDouble(Console.ReadLine());

                if (input % 2 == 0)
                    Console.WriteLine("True");
                else
                    Console.WriteLine("False");

            }
            Console.ReadLine();
        }
    }
}

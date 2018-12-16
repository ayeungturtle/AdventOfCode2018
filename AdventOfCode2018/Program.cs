using System;
using System.IO;

namespace AdventOfCode2018
{
    class Program
    {
        public static void Main(string[] args)
        {
            oneA();
        }

        // Each line in the input file contains a + or - operator and an integer.
        // Example lines:  +1, -2, +3, +1
        // Find the output after all operators have been run in line order.
        private static void oneA()
        {
            StreamReader stream = new StreamReader("inputOneA.txt");
            int line;
            int total = 0;
            while ((int.TryParse(stream.ReadLine(), out line)))
            {
                total += line;
            };
            Console.WriteLine(total);
        }
    }
}

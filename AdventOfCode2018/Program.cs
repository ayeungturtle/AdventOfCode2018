using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(OneB());
        }

        // Each line in the input file contains a + or - operator and an integer.
        // Example lines:  +1, -2, +3, +1
        // Find the output after all operators have been run in line order.
        private static void OneA()
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

        // Find the first frequency (total after each line) that repeats.
        // You might have to loop over the list to reach the first repeat.
        private static int OneB()
        {
            StreamReader stream = new StreamReader("inputOneA.txt");
            int line;
            int total = 0;
            HashSet<int> frequencies = new HashSet<int>();
            frequencies.Add(total);
            while (true)
            {
                // If there are no more lines left to read...
                if (stream.EndOfStream)
                {
                    // Reset the StreamReader back to the beginning.
                    stream = new StreamReader("inputOneA.txt");  //don't know why, but setting the postion back to zero actually set the position to 1024, so I just re-initiated the stream.
                }
                line = int.Parse(stream.ReadLine());

                total += line;
                if (frequencies.Contains(total))
                    return total;
                else
                    frequencies.Add(total);
            };
        }
    }
}

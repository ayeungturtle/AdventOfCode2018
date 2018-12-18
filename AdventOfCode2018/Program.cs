using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(GenerateCheckSum());
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

        // Calculate a "checksum" for a text file that
        // 1) counts the number of lines that contain exactly 2 of the same character (could also contain multiple pairs)
        // 2) counts the nubmer of lines that contain exactly 3 of the same character (could also contain multiple trios)
        // 3) multiples these two counts to produce the final checksum'
        private static int GenerateCheckSum()
        {
            StreamReader stream = new StreamReader("inputTwoA.txt");
            string line;
            int pairCount = 0;
            int trioCount = 0;
            bool hasPair;
            bool hasTrio;
            int repeatCount;
            char curChar;

            while ((line = stream.ReadLine()) != null)
            {
                hasPair = false;
                hasTrio = false;
                repeatCount = 1;
                line = String.Concat(line.OrderBy(c => c));
                curChar = line[0];

                for (int i = 1; i < line.Length; i++)
                {
                    if (line[i] == curChar)
                        repeatCount++;
                    if (line[i] != curChar || i == line.Length - 1)
                    {
                        if (repeatCount == 3)
                            hasTrio = true;
                        else if (repeatCount == 2)
                            hasPair = true;
                        repeatCount = 1;
                        curChar = line[i];
                    }
                }

                if (hasPair)
                    pairCount++;
                if (hasTrio)
                    trioCount++;

            }
            return pairCount * trioCount;
        }
    }
}

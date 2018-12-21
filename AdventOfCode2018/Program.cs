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
            Console.WriteLine(ThreeA());
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

        // 2A
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

        // 2B
        // Find two lines in the file that only differ by one character at the same string position.
        // Return the string with that differing character removed.
        // This solution is crude and innefficient.  Look into others.
        private static string DiffByOne()
        {
            string[] lines = File.ReadAllLines("inputTwoA.txt");
            string iLine;
            string jLine;
            int discrepancies;

            for (int i = 0; i < lines.Length - 1; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    discrepancies = 0;
                    iLine = lines[i];
                    jLine = lines[j];

                    if (iLine.Length != jLine.Length)
                        continue;

                    for (int k = 0; k < iLine.Length; k++)
                    {
                        if (iLine[k] != jLine[k])
                            discrepancies++;
                    }

                    if (discrepancies == 1)
                    {
                        for (int k = 0; k < iLine.Length; k++)
                        {
                            if (iLine[k] != jLine[k])
                            {
                                iLine = iLine.Remove(k, 1);
                                break;
                            }
                        }

                        return iLine;
                    }
                }
            }
            return "not found";
        }

        // 3A
        // https://adventofcode.com/2018/day/3
        // A line like #123 @ 3,2: 5x4 means that claim ID 123 specifies a rectangle 3 inches
        // from the left edge, 2 inches from the top edge, 5 inches wide, and 4 inches tall.
        // In a 1,000 by 1,000 grid, given the input data, find the total number of square inches
        // where there is overlap.
        private static int ThreeA()
        {
            string[] lines = File.ReadAllLines("input3.txt");
            bool[,] grid = new bool[1000, 1000];
            bool[,] duplicateGrid = new bool[1000, 1000];
            int duplicateCount = 0;
            int iStart;
            int jStart;
            int width;
            int height;
            int atLocation;
            int commaLocation;
            int colonLocation;
            int xLocation;

            foreach (string line in lines)
            {
                atLocation = line.IndexOf('@');
                commaLocation = line.IndexOf(',');
                colonLocation = line.IndexOf(':');
                xLocation = line.IndexOf('x');
                iStart = int.Parse(line.Substring(atLocation + 2, commaLocation - atLocation - 2));
                jStart = int.Parse(line.Substring(commaLocation + 1, colonLocation - commaLocation - 1));
                width = int.Parse(line.Substring(colonLocation + 2, xLocation - colonLocation - 2));
                height = int.Parse(line.Substring(xLocation + 1));

                for (int i = iStart; i < iStart + width; i++)
                {
                    for (int j = jStart; j < jStart + height; j++)
                    {
                        // If that coordinate has already been used...
                        if (grid[i,j])
                        {
                            // If that coordinate hasn't been accounted for as a duplicate...
                            if (duplicateGrid[i,j] == false)
                            {
                                duplicateCount++;
                                duplicateGrid[i, j] = true;
                            }
                        }
                        else
                        {
                            grid[i, j] = true;
                        }
                    }
                }
            }
            return duplicateCount;
        }
    }
}

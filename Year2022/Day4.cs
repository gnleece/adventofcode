using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.Year2022
{
    public static class Day4
    {
        private static readonly string INPUT_PATH = @"D:\Repos\adventofcode\Year2022\Input\input4.txt";

        private static readonly char[] SPLIT_CHARS = new char[] { '-', ',' };

        public static void CountFullyContainedPairs()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            var fullyContainedCount = 0;
            foreach (var line in inputLines)
            {
                var tokens = line.Split(SPLIT_CHARS);
                if (tokens.Length != 4)
                {
                    Console.WriteLine("bad input");
                    continue;
                }
                
                int leftMin = int.Parse(tokens[0]);
                int leftMax = int.Parse(tokens[1]);
                int rightMin = int.Parse(tokens[2]);
                int rightMax = int.Parse(tokens[3]);

                Console.WriteLine(line);
                if (leftMin < rightMin)
                {
                    // The left range is the lower range
                    if (rightMax <= leftMax)
                    {
                        fullyContainedCount++;
                    }
                }
                else if (rightMin < leftMin)
                {
                    // The right range is the lower range
                    if (leftMax <= rightMax)
                    {
                        fullyContainedCount++;
                    }
                }
                else
                {
                    // The ranges start at the same point, so one range must always fully contain the other
                    fullyContainedCount++;
                }
            }

            Console.WriteLine($"Fully overlapping pairs: {fullyContainedCount}");
        }

        public static void CountOverlappingPairs()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            var fullyContainedCount = 0;
            foreach (var line in inputLines)
            {
                var tokens = line.Split(SPLIT_CHARS);
                if (tokens.Length != 4)
                {
                    Console.WriteLine("bad input");
                    continue;
                }

                int leftMin = int.Parse(tokens[0]);
                int leftMax = int.Parse(tokens[1]);
                int rightMin = int.Parse(tokens[2]);
                int rightMax = int.Parse(tokens[3]);

                Console.WriteLine(line);
                if (leftMin < rightMin)
                {
                    // The left range is the lower range
                    if (rightMin <= leftMax)
                    {
                        fullyContainedCount++;
                    }
                }
                else if (rightMin < leftMin)
                {
                    // The right range is the lower range
                    if (leftMin <= rightMax)
                    {
                        fullyContainedCount++;
                    }
                }
                else
                {
                    // The ranges start at the same point, so they always overlap
                    fullyContainedCount++;
                }
            }

            Console.WriteLine($"Fully overlapping pairs: {fullyContainedCount}");
        }

    }
}

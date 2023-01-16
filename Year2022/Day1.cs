using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    public static class Day1
    {
        private static readonly string INPUT_PATH = @"D:\Repos\adventofcode\Year2022\Input\input1.txt";

        // Part 1
        public static void CalculateMaxCalories()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            int maxTotalCalories = 0;
            int curTotalCalories = 0;
            foreach (var line in inputLines)
            {
                if (string.Equals(line, string.Empty))
                {
                    // We're done with the current elf, compare to best elf
                    Console.WriteLine($"Current total: {curTotalCalories}, previous max: {maxTotalCalories}");
                    if (curTotalCalories > maxTotalCalories)
                    {
                        Console.WriteLine($"Updating max to {curTotalCalories}");
                        maxTotalCalories = curTotalCalories;
                    }
                    curTotalCalories = 0;
                }
                else
                {
                    // Keep adding to current elf
                    curTotalCalories += int.Parse(line);
                }
            }

            Console.WriteLine($"\nDone processing. Max calories = {maxTotalCalories}");
        }

        // Part 2
        public static void CalculateTop3Calories()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            // Calculate the calorie count for each elf
            List<int> allCalorieCounts = new List<int>();
            int curTotalCalories = 0;
            foreach (var line in inputLines)
            {
                if (string.Equals(line, string.Empty))
                {
                    // We're done with the current elf, add their total to the list
                    allCalorieCounts.Add(curTotalCalories);
                    curTotalCalories = 0;
                }
                else
                {
                    // Keep adding to current elf
                    curTotalCalories += int.Parse(line);
                }
            }

            // Sort all the counts and sum the top 3
            allCalorieCounts.Sort();
            var top3total = 0;
            var numEleves = allCalorieCounts.Count;
            for (int i = 0; i < 3; i++)
            {
                var curCount = allCalorieCounts[numEleves - i - 1];
                Console.WriteLine($"Adding {curCount}");
                top3total += curCount;
            }

            Console.WriteLine($"\nDone processing. Top 3 total = {top3total}");
        }
    }
}

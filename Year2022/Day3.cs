using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.Year2022
{
    public static class Day3
    {
        private static readonly string INPUT_PATH = @"D:\Repos\adventofcode\Year2022\Input\input3.txt";

        private const int ELF_GROUP_SIZE = 3;

        public static void CalculateTotalPriority()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            int totalPrioritySum = 0;
            foreach (var line in inputLines)
            {
                var compartmentSize = line.Length / 2;
                var compartment1 = line.Substring(0, compartmentSize);
                var compartment2 = line.Substring(compartmentSize, compartmentSize);

                var compartment1Items = new HashSet<char>();
                foreach (var item in compartment1)
                {
                    compartment1Items.Add(item);
                }

                char misplacedItem = 'a';
                foreach (var item in compartment2)
                {
                    if (compartment1Items.Contains(item))
                    {
                        misplacedItem = item;
                        break;
                    }
                }

                Console.WriteLine($"{compartment1} | {compartment2} | misplaced item = {misplacedItem}");

                totalPrioritySum += GetItemPriority(misplacedItem);
            }

            Console.WriteLine($"Total priority sum: {totalPrioritySum}");
        }

        public static void CalculateBadgePriority()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            int totalPrioritySum = 0;
            int currentGroupCount = 0;
            var elfItems = new Dictionary<char, int>();     // Count of how many elves have this item
            for (int i = 0; i < inputLines.Length; i++)
            {
                var currentItems = inputLines[i];
                var uniqueItems = currentItems.Distinct();  // Unique-ify the item list so that each elf is only counted once

                foreach (var item in uniqueItems)
                {
                    var currentCount = elfItems.GetValueOrDefault(item, 0);
                    elfItems[item] = currentCount + 1;
                }
                currentGroupCount++;

                if (currentGroupCount == ELF_GROUP_SIZE)
                {
                    // There should be exactly one item owned by every elf in the group
                    var badgeItem = elfItems.FirstOrDefault(x => x.Value == ELF_GROUP_SIZE).Key;

                    totalPrioritySum += GetItemPriority(badgeItem);

                    // Reset for the next group
                    elfItems.Clear();
                    currentGroupCount = 0;
                }
            }

            Console.WriteLine($"Total priority sum: {totalPrioritySum}");
        }

        private static int GetItemPriority(char item)
        {
            if (item >= 'a' && item <= 'z')
            {
                return (int)item - 96;
            }
            return (int)item - 38;
        }
    }
}

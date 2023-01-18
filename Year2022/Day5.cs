using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.Year2022
{
    public static class Day5
    {
        private static readonly string INPUT_PATH = @"D:\Repos\adventofcode\Year2022\Input\input5.txt";

        private const int CRATE_STRING_WIDTH = 4; // Each crate has format "[A] ", so length 4


        public static void GetTopCrates()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            List<Stack<char>> stacks = new List<Stack<char>>();

            int numStacks = inputLines[0].Length / CRATE_STRING_WIDTH + 1;
            for (int i = 0; i < numStacks; i++)
            {
                stacks.Add(new Stack<char>());
            }
            Console.WriteLine($"Num stacks: {numStacks}");

            int currentInputLineIndex = 0;

            // Parse the initial stack state
            var inputLinesStack = new Stack<string>();
            var inputLine = inputLines[currentInputLineIndex];
            while (inputLine.Contains('['))
            {
                inputLinesStack.Push(inputLine);
                currentInputLineIndex++;
                inputLine = inputLines[currentInputLineIndex];
            }

            // The stacks got pushed from top to bottom, so they need to be flipped
            while (inputLinesStack.Count > 0)
            {
                var row = inputLinesStack.Pop();
                var cratesForRow = GetCratesArrayForRow(row, numStacks);
                for (int i = 0; i < numStacks; i++)
                {
                    var crate = cratesForRow[i];
                    if (crate != ' ')
                    {
                        stacks[i].Push(crate);
                    }
                }
            }

            PrintStacksState(stacks);
            Console.WriteLine();

            // Parse the moves list
            for (; currentInputLineIndex < inputLines.Count(); currentInputLineIndex++)
            {
                inputLine = inputLines[currentInputLineIndex];
                if (inputLine.Length > 0 && inputLine[0] =='m')
                {
                    var tokens = inputLine.Split(' ');
                    if (tokens.Length != 6)
                    {
                        Console.WriteLine($"bad input: {inputLine}");
                        continue;
                    }

                    var numToMove = int.Parse(tokens[1]);
                    var sourceStackIndex = int.Parse(tokens[3]) - 1;
                    var destStackIndex = int.Parse(tokens[5]) - 1;

                    for (int i = 0; i < numToMove; i++)
                    {
                        var crate = stacks[sourceStackIndex].Pop();
                        stacks[destStackIndex].Push(crate);
                    }
                }
                Console.WriteLine(inputLine);
                //PrintStacksState(stacks);
                //Console.WriteLine();
                //Console.WriteLine();

                //System.Console.ReadKey();
            }

            // Get the top crate from each stack
            StringBuilder sb = new StringBuilder();
            foreach (var stack in stacks)
            {
                var success = stack.TryPeek(out char crate);
                var crateChar = success ? crate : ' ';
                sb.Append(crateChar.ToString());
            }

            PrintStacksState(stacks);
            Console.Write($"Top crates: {sb.ToString()}");
        }

        private static char[] GetCratesArrayForRow(string row, int numStacks)
        {
            char[] crates = new char[numStacks];
            for (int i = 0; i < numStacks; i++)
            {
                crates[i] = row[i*CRATE_STRING_WIDTH + 1];
            }
            return crates;
        }

        private static void PrintStacksState(List<Stack<char>> stacks)
        {
            int maxStackHeight = 0;
            foreach(var stack in stacks)
            {
                int height = stack.Count;
                if (height > maxStackHeight )
                {
                    maxStackHeight = height;
                }
            }

            for (int i = maxStackHeight - 1; i >= 0; i--)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var stack in stacks)
                {
                    var crateString = GetCrateStringAtPosition(stack, i);
                    sb.Append(crateString);
                }

                Console.WriteLine(sb.ToString());
            }
        }

        private static string GetCrateStringAtPosition(Stack<char> stack, int position)
        {
            if (position < stack.Count)
            {
                var flippedPosition = stack.Count - position - 1;
                return $"[{stack.ElementAt(flippedPosition)}] ";
            }
            else
            {
                return "    ";
            }    
        }
    }
}

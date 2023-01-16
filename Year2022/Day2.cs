using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.Year2022
{
    public static class Day2
    {
        private static readonly string INPUT_PATH = @"D:\Repos\adventofcode\Year2022\Input\input2.txt";

        private enum MoveType
        {
            None = -1,
            Rock = 0,
            Paper = 1,
            Scissors = 2,
            NUM_MOVES = 3
        }

        private enum OutcomeType
        {
            None = -1,
            Tie = 0,
            Win = 1,
            Loss = 2,
            NUM_OUTCOMES = 3
        }

        private static Dictionary<MoveType, int> MovePoints = new Dictionary<MoveType, int>()
        {
            { MoveType.Rock, 1 },
            { MoveType.Paper, 2 },
            { MoveType.Scissors, 3 },
        };

        private static Dictionary<OutcomeType, int> OutcomePoints = new Dictionary<OutcomeType, int>()
        {
            { OutcomeType.Loss, 0 },
            { OutcomeType.Tie, 3 },
            { OutcomeType.Win, 6 },
        };


        // Part 1
        public static void CalculateScorePart1()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            var totalScore = 0;
            foreach (var line in inputLines)
            {
                var tokens = line.Split(' ');
                if (tokens.Length != 2)
                {
                    Console.WriteLine($"Error parsing input line: {line}");
                    continue;
                }

                var opponentMove = ParseMove(tokens[0]);
                var myMove = ParseMove(tokens[1]);

                var outcome = GetOutcome(myMove, opponentMove);

                var roundScore = GetSelectionScore(myMove) + GetOutcomeScore(outcome);
                totalScore += roundScore;
            }

            Console.WriteLine($"My total score: {totalScore}");
        }

        // Part 2
        public static void CalculateScorePart2()
        {
            string[] inputLines = System.IO.File.ReadAllLines(INPUT_PATH);

            var totalScore = 0;
            foreach (var line in inputLines)
            {
                var tokens = line.Split(' ');
                if (tokens.Length != 2)
                {
                    Console.WriteLine($"Error parsing input line: {line}");
                    continue;
                }

                var opponentMove = ParseMove(tokens[0]);
                var outcome = ParseOutcome(tokens[1]);

                var myMove = GetMoveFromDesiredOutcome(opponentMove, outcome);

                var roundScore = GetSelectionScore(myMove) + GetOutcomeScore(outcome);
                totalScore += roundScore;
            }

            Console.WriteLine($"My total score: {totalScore}");
        }

        private static MoveType ParseMove(string moveString)
        {
            switch(moveString)
            {
                case "A":
                case "X":
                    return MoveType.Rock;
                case "B":
                case "Y":
                    return MoveType.Paper;
                case "C":
                case "Z":
                    return MoveType.Scissors;
                default:
                    return MoveType.None;
                    
            }
        }

        private static OutcomeType ParseOutcome(string outcomeString)
        {
            switch (outcomeString)
            {
                case "X": return OutcomeType.Loss;
                case "Y": return OutcomeType.Tie;
                case "Z": return OutcomeType.Win;
                default: return OutcomeType.None;
            }
        }

        private static int GetSelectionScore(MoveType move)
        {
            return MovePoints[move];
        }

        private static int GetOutcomeScore(OutcomeType outcome)
        {
            return OutcomePoints[outcome];
        }

        private static OutcomeType GetOutcome(MoveType myMove, MoveType opponentMove)
        {
            var delta = (int)myMove - (int)opponentMove;
            var numOutcomes = (int)OutcomeType.NUM_OUTCOMES;
            var outcome = (OutcomeType)((delta + numOutcomes) % numOutcomes);

            return outcome;
        }

        private static MoveType GetMoveFromDesiredOutcome(MoveType opponentMove, OutcomeType outcome)
        {
            switch(outcome)
            {
                case OutcomeType.Loss: return ShiftMove(opponentMove, -1);
                case OutcomeType.Tie: return opponentMove;
                case OutcomeType.Win: return ShiftMove(opponentMove, 1);
            }
            return MoveType.None;
        }

        private static MoveType ShiftMove(MoveType move, int shiftAmount)
        {
            var numMoves = (int)MoveType.NUM_MOVES;
            return (MoveType)(((int)move + shiftAmount + numMoves) % numMoves);
        }
    }
}

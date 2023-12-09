using System.Diagnostics;
using System.Text.RegularExpressions;
using Utils;

namespace Day8
{
    class Program
    {
        struct NextInstructions
        {
            public string left;
            public string right;

            public NextInstructions(string l, string r)
            {
                left = l;
                right = r;
            }

            public string GetNextInstruction(char direction)
            {
                return direction == 'L' ? left : right;
            }
        }

        private static Dictionary<string, NextInstructions> allInstructions = new Dictionary<string, NextInstructions>();
        private static string allDirections;

        static void Main(string[] args)
        {
            var inputs = AoCUtils.ReadDaysInput(8);
            ParseInputs(inputs);
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
            
        }

        static void ParseInputs(List<string> inputs)
        {
            allDirections = inputs[0];

            for (int i = 2; i < inputs.Count; i++)
            {
                var allInputs = Regex.Matches(inputs[i], @"[A-Z]{3}");
                allInstructions[allInputs[0].Value] = new NextInstructions(allInputs[1].Value, allInputs[2].Value);
            }
        }

        private static long Part1()
        {
            string currentInstruction = "AAA";
            int currentDirectionIndex = 0;
            int steps = 0;
            while (currentInstruction != "ZZZ")
            {
                currentInstruction = allInstructions[currentInstruction]
                    .GetNextInstruction(allDirections[currentDirectionIndex]);
                currentDirectionIndex = currentDirectionIndex + 1 == allDirections.Length ? 0 : currentDirectionIndex + 1;
                steps++;
            }

            return steps;
        }
        
        private static long Part2()
        {
            List<string> currentInstructions = new List<string>();
            List<string> targetInstructions = new List<string>();

            foreach (var instructionKeyVal in allInstructions)
            {
                if (instructionKeyVal.Key[2] == 'A')
                {
                    currentInstructions.Add(instructionKeyVal.Key);
                    targetInstructions.Add($"{instructionKeyVal.Key[0]}{instructionKeyVal.Key[1]}Z");
                }
            }
            
            int currentDirectionIndex = 0;
            int steps = 0;
            while (true)
            {
                bool areAllZs = true;
                for (int i = 0; i < currentInstructions.Count; i++)
                {
                    currentInstructions[i] = allInstructions[currentInstructions[i]]
                        .GetNextInstruction(allDirections[currentDirectionIndex]);
                    if (currentInstructions[i] != targetInstructions[i])
                    {
                        areAllZs = false;
                    }
                }
                currentDirectionIndex = currentDirectionIndex + 1 == allDirections.Length ? 0 : currentDirectionIndex + 1;
                steps++;
                if (areAllZs == true)
                {
                    break;
                }
            }

            return steps;
        }

    }
}
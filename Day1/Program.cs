using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Utils;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var todaysInput = AoCUtils.ReadDaysInput(1);
            long sum = 0;
            foreach (var line in todaysInput)
            {
                int combinedNumber = Convert.ToInt32($"{FindFirstNumber(line)}{FindLastNumber(line)}");
                sum += combinedNumber;
                FindFirstNumberP2(line);
            }
            Console.WriteLine(sum);
            
            // Part 2

        }

        static char FindFirstNumber(string line)
        {
            foreach (var c in line)
            {
                if (char.IsNumber(c))
                {
                    return c;
                }
            }

            return '\0';
        }

        static char FindLastNumber(string line)
        {
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsNumber(line[i]))
                {
                    return line[i];
                }
            }
            return '\0';
        }

        static char FindFirstNumberP2(string line)
        {
            var matchString = "one|two|three|four|five|six|seven|eight|nine";
            var allLiterals = Regex.Matches(line, matchString);

            int smallestMatch = Int32.MaxValue, biggestMatch = -1;
            foreach (Match match in allLiterals)
            {
                if (match.Index < smallestMatch)
                {
                    smallestMatch = match.Index;
                }
            }
            
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsNumber(line[i]))
                {
                    return line[i];
                }
                
                // now check if starts with the letter
                // Possible variants: one, two, three, four, five, six, seven, eight, and nine
            }

            return '\0';
        }
    }
}
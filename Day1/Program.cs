using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Utils;

namespace Day1
{
    class Program
    {
        struct NumberIndexLengthPair
        {
            public int index;
            public int length;
            public char numericalValue;

            private static readonly Dictionary<string, char> stringIntPairs = new Dictionary<string, char>()
            {
                {"one", '1'},
                {"two", '2'},
                {"three", '3'},
                {"four", '4'},
                {"five", '5'},
                {"six", '6'},
                {"seven", '7'},
                {"eight", '8'},
                {"nine", '9'}
            };

            public void SetNumericalValueFromString(string st)
            {
                numericalValue = stringIntPairs[st];
            }
            
            
        }
        
        static void Main(string[] args)
        {
            var todaysInput = AoCUtils.ReadDaysInput(1);
            long sum = 0;
            // foreach (var line in todaysInput)
            // {
            //     int combinedNumber = Convert.ToInt32($"{FindFirstNumber(line)}{FindLastNumber(line)}");
            //     sum += combinedNumber;
            // }
            // Console.WriteLine(sum);
            
            // Part 2
            sum = 0;
            string[] numbersWords =
            {
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
            };
            foreach (var line in todaysInput)
            {
                var matchString = "one|two|three|four|five|six|seven|eight|nine";
                var allLiterals = Regex.Matches(line, matchString);

                // NumberIndexLengthPair smallestMatch = new NumberIndexLengthPair{index = Int32.MaxValue, length = 0 }, 
                //     biggestMatch = new NumberIndexLengthPair{index = -1, length = 0 };
                // foreach (Match match in allLiterals)
                // {
                //     if (match.Index < smallestMatch.index)
                //     {
                //         smallestMatch.index = match.Index;
                //         smallestMatch.length = match.Length;
                //         smallestMatceh.length = match.Length;
                //         smallestMatch.SetNumericalValueFromString(line.Substring(smallestMatch.index, smallestMatch.length));
                //     }
                //
                //     if (match.Index > biggestMatch.index)
                //     {
                //         biggestMatch.index = match.Index;
                //         biggestMatch.length = match.Length;
                //         biggestMatch.SetNumericalValueFromString(line.Substring(biggestMatch.index, biggestMatch.length));
                //     }
                // }

                NumberIndexLengthPair smallestMatch = new NumberIndexLengthPair{index = Int32.MaxValue, length = 0 }, 
                    biggestMatch = new NumberIndexLengthPair{index = -1, length = 0 };
                foreach (var word in numbersWords)
                {
                    var indexOf = line.IndexOf(word);
                    var lastIndexOf = line.LastIndexOf(word);
                    if (indexOf < smallestMatch.index && indexOf > -1)
                    {
                        smallestMatch.index = indexOf;
                        smallestMatch.length = word.Length;
                        smallestMatch.SetNumericalValueFromString(word);
                    }

                    if (lastIndexOf > biggestMatch.index && lastIndexOf > -1)
                    {
                        biggestMatch.index = lastIndexOf;
                        biggestMatch.length = word.Length;
                        biggestMatch.SetNumericalValueFromString(word);
                    }
                }
                
                
                int combinedNumber = Convert.ToInt32($"{FindFirstNumberP2(line, smallestMatch)}{FindLastNumberP2(line, biggestMatch)}");
                sum += combinedNumber;
            }
            Console.WriteLine(sum);
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

        private static char FindLastNumberP2(string line, NumberIndexLengthPair biggestNumber)
        {
            for (int i = line.Length - 1; i > biggestNumber.index + biggestNumber.length - 1; i--)
            {
                if (char.IsNumber(line[i]))
                {
                    return line[i];
                }
            }

            return biggestNumber.numericalValue;
        }
        
        static char FindFirstNumberP2(string line, NumberIndexLengthPair smallestNumber)
        {
            for (int i = 0; i < smallestNumber.index; i++)
            {
                if (char.IsNumber(line[i]))
                {
                    return line[i];
                }
            }

            return smallestNumber.numericalValue;
        }
    }
}
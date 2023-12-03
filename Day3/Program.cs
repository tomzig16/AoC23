using System.Text.RegularExpressions;
using static Utils.AoCUtils;

namespace Day2
{
    class Program
    {
        static void Main(String[] args)
        {
            // int nOfRows, nOfCols;
            // var inputTable = ReadDaysInputAs2DArray(3, out nOfRows, out nOfCols);
            var inputTable = ReadDaysInput(3);
            
            Console.Write(Part1(inputTable));
        }

        private static long Part1(List<string> inputTable)
        {
            string regexForAnyNumber = @"\d+";
            long totalSum = 0;
            
            for (int i = 0; i < inputTable.Count; i++)
            {
                Console.WriteLine($"New row: {i}");
                var allMatchesWithNumbers = Regex.Matches(inputTable[i], regexForAnyNumber);
                if (allMatchesWithNumbers.Count != 0)
                {
                    // Regex wouldnt work if there are 2 symbols next to each other (just like day 1)
                    foreach (Match match in allMatchesWithNumbers)
                    {
                        if (CheckIfHasASymbolAroundIt(inputTable, i, match.Index, match.Length))
                        {
                            Console.WriteLine(match.Value);
                            totalSum += int.Parse(match.Value);
                        }
                    }

                    
                }
            }

            return totalSum;
        }

        private static bool CheckIfHasASymbolAroundIt(List<string> inputTable, int matchRow, int firstMatchIndex, int matchLength)
        {
            string regexForSymbols = @"[!@#$%^&*()_+=/-]";
            int startingMatch = firstMatchIndex > 0 ? firstMatchIndex - 1 : 0;
            int lengthToCheck =  firstMatchIndex + matchLength + 1 > inputTable[matchRow].Length ? matchLength + 1 : matchLength + 2 ;
            MatchCollection allMatches;
            string checkedSubstring;
            // Add all top numbers
            if (matchRow > 0)
            {
                checkedSubstring = inputTable[matchRow - 1].Substring(startingMatch, lengthToCheck).Trim();
                allMatches = Regex.Matches(checkedSubstring, regexForSymbols);
                if (allMatches.Count > 0) return true;
            }
            // Same row numbers
            checkedSubstring = inputTable[matchRow].Substring(startingMatch, lengthToCheck).Trim();
            allMatches = Regex.Matches(checkedSubstring, regexForSymbols);
            if (allMatches.Count > 0) return true;
            // Add all bottom numbers
            if (matchRow < inputTable.Count - 1)
            {
                checkedSubstring = inputTable[matchRow + 1].Substring(startingMatch, lengthToCheck).Trim();
                allMatches = Regex.Matches(checkedSubstring, regexForSymbols);
                if (allMatches.Count > 0) return true;
            }

            return false;
        }
        
        // saving for part 2 just in case
        // private static int CheckIfHasASymbolAroundIt(List<string> inputTable, int symbolRow, int firstMatchIndex, int matchLength)
        // {
        //     int sum = 0;
        //     var TryParseAndAdd = new Action<char>(c =>
        //     {
        //         int nForTryParse;
        //         if (int.TryParse(c.ToString(), out nForTryParse))
        //         {
        //             sum += nForTryParse;
        //         }
        //     });
        //     
        //     // Add all top numbers
        //     if (symbolRow > 0)
        //     {
        //         if (symbolCol > 0)
        //         {
        //             TryParseAndAdd(inputTable[symbolRow - 1][symbolCol - 1]); // Top left
        //         }
        //         TryParseAndAdd(inputTable[symbolRow - 1][symbolCol]); // Top center
        //         if (symbolCol < inputTable[symbolRow - 1].Length - 1)
        //         {
        //             TryParseAndAdd(inputTable[symbolRow - 1][symbolCol + 1]); // Top right
        //         }
        //     }
        //     // Same row numbers
        //     if (symbolCol > 0)
        //     {
        //         TryParseAndAdd(inputTable[symbolRow][symbolCol - 1]); // Left
        //     }
        //     if (symbolCol < inputTable[symbolRow].Length - 1)
        //     {
        //         TryParseAndAdd(inputTable[symbolRow ][symbolCol + 1]); // Right
        //     }
        //     // Add all bottom numbers
        //     if (symbolRow < inputTable.Count - 1)
        //     {
        //         if (symbolCol > 0)
        //         {
        //             TryParseAndAdd(inputTable[symbolRow + 1][symbolCol - 1]); // Bot left
        //         }
        //         TryParseAndAdd(inputTable[symbolRow + 1][symbolCol]); // Bot center
        //         if (symbolCol < inputTable[symbolRow + 1].Length - 1)
        //         {
        //             TryParseAndAdd(inputTable[symbolRow + 1][symbolCol + 1]); // Bot right
        //         }
        //     }
        //
        //     return sum;
        // }
    }
}
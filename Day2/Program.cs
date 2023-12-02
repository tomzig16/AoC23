using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        private static int maxRed = 12;
        private static int maxBlue = 14;
        private static int maxGreen = 13;
        private static string matchString = @"(\d*) (blue|green|red)";
        
        static void Main(string[] args)
        {
            var todaysInput = Utils.AoCUtils.ReadDaysInput(2);
            
            Console.WriteLine(Part1(todaysInput));
            Console.WriteLine(Part2(todaysInput));
        }

        static int Part1(List<string> input)
        {
            int gameIdSum = 0;
            foreach (var line in input)
            {
                string[] GameIDandValues = line.Split(':');
                int gameID = GetGameID(GameIDandValues[0]);
                
                bool shouldContinueForGame = true;
                
                foreach (string eachSet in GameIDandValues[1].Split(';'))
                {
                    var allMatches = Regex.Matches(eachSet, matchString);
                    int availableRed = maxRed;
                    int availableBlue = maxBlue;
                    int availableGreen = maxGreen;
                    foreach (Match match in allMatches)
                    {
                        var number = int.Parse(match.Groups[1].ToString());
                        var color = match.Groups[2];

                        switch (color.ToString())
                        {
                            case "red":
                                availableRed -= number;
                                break;
                            case "green":
                                availableGreen -= number;
                                break;
                            case "blue":
                                availableBlue -= number;
                                break;
                        }

                        if (availableGreen < 0 || availableBlue < 0 || availableRed < 0)
                        {
                            shouldContinueForGame = false;
                            break;
                        }
                    }

                    if (!shouldContinueForGame)
                    {
                        break;
                    }
                }
                if (shouldContinueForGame)
                {
                    gameIdSum += gameID;
                }
                
            }

            return gameIdSum;
        }

        static long Part2(List<string> input)
        {
            long powerSum = 0;
            foreach (var line in input)
            {
                string[] GameIDandValues = line.Split(':');

                int gameMinRed = 0;
                int gameMinGreen = 0;
                int gameMinBlue = 0;
                
                foreach (string eachSet in GameIDandValues[1].Split(';'))
                {
                    var allMatches = Regex.Matches(eachSet, matchString);
                    foreach (Match match in allMatches)
                    {
                        var number = int.Parse(match.Groups[1].ToString());
                        var color = match.Groups[2];

                        switch (color.ToString())
                        {
                            case "red":
                                if (gameMinRed < number)
                                {
                                    gameMinRed = number;
                                }
                                break;
                            case "green":
                                if (gameMinGreen < number)
                                {
                                    gameMinGreen = number;
                                }
                                break;
                            case "blue":
                                if (gameMinBlue < number)
                                {
                                    gameMinBlue = number;
                                }
                                break;
                        }
                    }
                    
                }

                powerSum += (gameMinRed * gameMinGreen * gameMinBlue);

            }

            return powerSum;
        }
        
        static int GetGameID(string line)
        {
            string n = line.Substring("Game ".Length, line.Length - "Game ".Length);
            return int.Parse(n);
        }
    }
}
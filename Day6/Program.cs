using System.Text.RegularExpressions;
using Utils;

namespace Day6
{
    class Program
    {
        struct RaceDetails(string durationString, string distanceString)
        {
            public long duration = long.Parse(durationString);
            public long distance = long.Parse(distanceString);
        }
        static void Main(string[] args)
        {
            var inputs = AoCUtils.ReadDaysInput(6);
            
            Console.WriteLine(Part1(inputs));
            Console.WriteLine(Part2(inputs));
            
        }

        private static long Part1(List<string> inputs)
        {
            long resultMult = 1;
            List<RaceDetails> allRaces = GetRaceDetailsFromInput(inputs);

            foreach (var singleRace in allRaces)
            {
                resultMult *= SolveSingleRace(singleRace);
            }

            return resultMult;
        }

        private static List<RaceDetails> GetRaceDetailsFromInput(List<string> inputs)
        {
            List<RaceDetails> details = new List<RaceDetails>();
            var matchString = @"\d+";
            var allDurations = Regex.Matches(inputs[0], matchString);
            var allDistances = Regex.Matches(inputs[1], matchString);
            for (int i = 0; i < allDurations.Count; i++)
            {
                details.Add(new RaceDetails(allDurations[i].Value, allDistances[i].Value));
            }

            return details;
        }

        private static long SolveSingleRace(RaceDetails singleRace)
        {
            // Formula for distance is distance = hold * duration
            // duration = total - hold
            // distance = hold * (total - hold)
            // distance = - hold^2 + hold * total
            // Final equation for our distance is y = -x^2 + xT
            // y - our distance traveled
            // x - how long we held the button (it is the same as the speed)
            // T - total race duration
            
            // We aim for minimal distance which is linear function y' = N
            // From that we can calculate the min and max distances and hold durations for each integer in
            // y > y'
            // -x^2 + xT > N
            // -x^2 + xT - N > 0
            // D = ( -T ± sqrt(T^2 - 4 * (-1) * (-N) ) / -2
            
            long T = singleRace.duration;
            long N = singleRace.distance;

            double Dpos = (-T + Math.Sqrt((T * T) - (4 * (-1) * (-N)))) / -2; 
            double Dneg = (-T - Math.Sqrt((T * T) - (4 * N))) / -2;

            // + 1 / -1 and then flooring/ceiling ensures that we get correct result excluding the "tie" scenario for the race
            long finalDPos = (long)Math.Floor(Dpos + 1);  
            long finalDNeg = (long)Math.Ceiling(Dneg - 1);
            long possibleWaysToWin = finalDNeg - finalDPos + 1;
            return possibleWaysToWin;
        }
        
        private static long Part2(List<string> inputs)
        {
            var matchString = @"\d+";
            var allDurations = Regex.Matches(inputs[0], matchString);
            var allDistances = Regex.Matches(inputs[1], matchString);
            string finalDuration = "";
            string finalDistance = "";
            for (int i = 0; i < allDurations.Count; i++)
            {
                finalDistance += allDistances[i];
                finalDuration += allDurations[i];
            }

            return SolveSingleRace(new RaceDetails(finalDuration, finalDistance));
        }
    }
}
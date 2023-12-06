using System.Text.RegularExpressions;
using Utils;

namespace Day6
{
    class Program
    {
        struct RaceDetails(string durationString, string distanceString)
        {
            public int duration = int.Parse(durationString);
            public int distance = int.Parse(distanceString);
        }
        static void Main(string[] args)
        {
            var inputs = AoCUtils.ReadDaysInput(6);
            
            Console.WriteLine(Part1(inputs));
            Console.WriteLine(Part2(inputs));
            
        }

        private static int Part1(List<string> inputs)
        {
            int resultMult = 1;
            List<RaceDetails> allRaces = GetRaceDetailsFromInput(inputs);
            
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

            foreach (var singleRace in allRaces)
            {
                var T = singleRace.duration;
                var N = singleRace.distance;

                double Dpos = (-T + Math.Sqrt((T * T) - (4 * (-1) * (-N)))) / -2; 
                double Dneg = (-T - Math.Sqrt((T * T) - (4 * N))) / -2;

                // + 1 / -1 and then flooring/ceiling ensures that we get correct result excluding the "tie" scenario for the race
                int finalDPos = (int)MathF.Floor((float)Dpos + 1);  
                int finalDNeg = (int)MathF.Ceiling((float)Dneg - 1);
                int possibleWaysToWin = finalDNeg - finalDPos + 1;
                resultMult *= possibleWaysToWin;
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

        private static int Part2(List<string> inputs)
        {
            int nOfCardsToAdd = 0;
            return nOfCardsToAdd;
        }
    }
}
using Utils;

namespace Day5
{
    class Program
    {
        class ConvertingMapping
        {
            private List<long> sourceIndices = new List<long>();
            private List<long> destinationIndices = new List<long>();
            private List<long> lenghts = new List<long>();

            public void AddValues(string[] threeValues)
            {
                destinationIndices.Add(long.Parse(threeValues[0]));
                sourceIndices.Add(long.Parse(threeValues[1]));
                lenghts.Add(long.Parse(threeValues[2]));
            }

            public long GetNextMapping(long input)
            {
                for (var i = 0; i < sourceIndices.Count; i++)
                {
                    var source = sourceIndices[i];
                    if (source <= input && input < source + lenghts[i])
                    {
                        long stride = input - source;
                        return destinationIndices[i] + stride;
                    }
                }

                return input;
            }
        }
        
        
        static void Main(string[] args)
        {
            var inputs = AoCUtils.ReadDaysInput(5);

            Console.WriteLine(Part1(inputs));
            // Console.WriteLine(Part2(inputs));
            
        }

        private static long Part1(List<string> inputs)
        {
            long minResult = int.MaxValue;
            List<long> seeds = new List<long>();
            Dictionary<string, ConvertingMapping> allMappings = new Dictionary<string, ConvertingMapping>();
            ParseInput(inputs, out seeds, out allMappings);

            foreach (var seed in seeds)
            {
                long soil = allMappings["seed-to-soil"].GetNextMapping(seed);
                long fertilizer = allMappings["soil-to-fertilizer"].GetNextMapping(soil);
                long water = allMappings["fertilizer-to-water"].GetNextMapping(fertilizer);
                long light = allMappings["water-to-light"].GetNextMapping(water);
                long temp = allMappings["light-to-temperature"].GetNextMapping(light);
                long humidity = allMappings["temperature-to-humidity"].GetNextMapping(temp);
                long location = allMappings["humidity-to-location"].GetNextMapping(humidity);
                Console.WriteLine($"Seed {seed} result is {location}");
                if (minResult > location)
                {
                    minResult = location;
                }
            }

            return minResult;

        }

        private static void ParseInput(List<string> inputs, out List<long> seeds, out Dictionary<string, ConvertingMapping> allMappings)
        {
            seeds = inputs[0].Split(':')[1].Trim().Split(" ").Select(long.Parse).ToList();
            allMappings = new Dictionary<string, ConvertingMapping>();
            // {
            //     { "seed-to-soil", new Dictionary<long, long>() },
            //     { "soil-to-fertilizer", new Dictionary<long, long>() },
            //     { "fertilizer-to-water", new Dictionary<long, long>() },
            //     { "water-to-light", new Dictionary<long, long>() },
            //     { "light-to-temperature", new Dictionary<long, long>() },
            //     { "temperature-to-humidity ", new Dictionary<long, long>() },
            //     { "humidity-to-location", new Dictionary<long, long>() },
            // };

            string currentDictionaryKey = "";
            for (int i = 1; i < inputs.Count; i++)
            {
                if (inputs[i].Contains("map"))
                {
                    currentDictionaryKey = inputs[i].Split(' ')[0];
                    allMappings[currentDictionaryKey] = new ConvertingMapping();
                    continue;
                }

                string[] allNumbers = inputs[i].Split(' ');
                if (allNumbers.Length == 3)
                {
                    allMappings[currentDictionaryKey].AddValues(allNumbers);
                }

            }
        }
        
        private static int Part2(List<string> inputs)
        {
            return 0;
        }
    }
}
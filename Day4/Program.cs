using Utils;

namespace Day4
{
    class Program
    {
        static StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        static void Main(string[] args)
        {
            var inputs = AoCUtils.ReadDaysInput(4);

            Console.WriteLine(Part1(inputs));
            Console.WriteLine(Part2(inputs));
            
        }

        private static int Part1(List<string> inputs)
        {
            int sumOfPoints = 0;
            foreach (var line in inputs)
            {
                int indexOfColon = line.IndexOf(':');
                string actualValues = line.Substring(indexOfColon + 1);
                string[] winningNumbersVsInputs = actualValues.Split('|');
                
                string[] winningNumbersSplit = winningNumbersVsInputs[0].Trim().Split(' ', splitOptions);
                string[] inputNumbersSplit = winningNumbersVsInputs[1].Trim().Split(' ', splitOptions);

                int winningNumberCount = 0;
                foreach (var winningNumber in winningNumbersSplit)
                {
                    foreach (var inputNumber in inputNumbersSplit)
                    {
                        if (winningNumber == inputNumber)
                        {
                            winningNumberCount++;
                            break;
                        }
                    }
                }
                
                int pointsForCard = winningNumberCount > 1 ? 1 << (winningNumberCount - 1) : winningNumberCount;
                sumOfPoints += pointsForCard;


            }

            return sumOfPoints;
        }
        
        private static int Part2(List<string> inputs)
        {
            int nOfCardsToAdd = 0;
            Dictionary<int, int> cardsAndAmount = new Dictionary<int, int>();
            for (int i = 0; i < inputs.Count; i++)
            {
                if (!cardsAndAmount.ContainsKey(i))
                {
                    cardsAndAmount[i] = 0;
                }

                cardsAndAmount[i]++;
                
                int nOfWins = 0;
                int indexOfColon = inputs[i].IndexOf(':');
                string actualValues = inputs[i].Substring(indexOfColon + 1);
                string[] winningNumbersVsInputs = actualValues.Split('|');
                
                string[] winningNumbersSplit = winningNumbersVsInputs[0].Trim().Split(' ', splitOptions);
                string[] inputNumbersSplit = winningNumbersVsInputs[1].Trim().Split(' ', splitOptions);
                
                foreach (var winningNumber in winningNumbersSplit)
                {
                    foreach (var inputNumber in inputNumbersSplit)
                    {
                        if (winningNumber == inputNumber)
                        {
                            nOfWins++;
                            break;
                        }
                    }
                }

                for (int j = 0; j < nOfWins; j++)
                {
                    if (!cardsAndAmount.ContainsKey(i + j + 1))
                    {
                        cardsAndAmount[i + j + 1] = 0;
                    }

                    cardsAndAmount[i + j + 1] += cardsAndAmount[i];
                }

                nOfCardsToAdd += cardsAndAmount[i];

            }

            return nOfCardsToAdd;
        }
    }
}
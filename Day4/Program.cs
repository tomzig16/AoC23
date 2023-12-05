using Utils;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = AoCUtils.ReadDaysInput(4);

            Console.WriteLine(Part1(inputs));
            
        }

        private static int Part1(List<string> inputs)
        {
            int sumOfPoints = 0;
            foreach (var line in inputs)
            {
                int indexOfColon = line.IndexOf(':');
                string actualValues = line.Substring(indexOfColon + 1);
                string[] winningNumbersVsInputs = actualValues.Split('|');

                string[] winningNumbersSplit = winningNumbersVsInputs[0].Trim().Split(' ');
                string[] inputNumbersSplit = winningNumbersVsInputs[1].Trim().Split(' ');

                int winningNumberCount = 0;
                foreach (var winningNumber in winningNumbersSplit)
                {
                    if (winningNumber == "") continue;
                    foreach (var inputNumber in inputNumbersSplit)
                    {
                        if(inputNumber == "") continue;
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
    }
}
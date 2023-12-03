namespace Utils;

public class AoCUtils
{
    public static string fileName = "input.txt";
    
    public static List<string> ReadDaysInput(int day)
    {
        var pathToFile = AppDomain.CurrentDomain.BaseDirectory.Split($"Day{day}");
        return new List<string>(
            File.ReadAllLines(
                Path.Combine(pathToFile[0], $"Day{day}", $"{fileName}"))
            );
    } 
    
    public static char[,] ReadDaysInputAs2DArray(int day, out int nOfRows, out int nOfCols)
    {
        var dataInList = ReadDaysInput(day);
        char[,] allSymbols = new char[dataInList.Count, dataInList[0].Length];
        nOfRows = dataInList.Count;
        nOfCols = dataInList[0].Length;
        
        for (int i = 0; i < dataInList.Count; i++)
        {
            for (int j = 0; j < dataInList[i].Length; j++)
            {
                allSymbols[i, j] = dataInList[i][j];
            }
        }

        return allSymbols;
    } 
}
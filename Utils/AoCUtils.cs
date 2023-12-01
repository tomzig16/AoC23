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
}
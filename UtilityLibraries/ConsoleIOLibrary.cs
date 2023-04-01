namespace UtilityLibraries;
public static class ConsoleIOLibrary
{
    //ввод целого числа с консоли с обработкой ошибок
    public static int GetInt(string inputMessage, string errorMessage, Predicate<int> condition)
    {
        int result;
        bool isCorrect;
        do
        {
            ColorDisplay(inputMessage, ConsoleColor.Green);
            isCorrect = int.TryParse(Console.ReadLine(), out result) && condition(result);
            if (!isCorrect)
                ColorDisplay(errorMessage, ConsoleColor.Red);
        } while (!isCorrect);
        return result;
    }
    //ввода символа с консоли с обработкой ошибок
    public static char GetChar(string inputMessage, string errorMessage, Predicate<char> condition)
    {
        char result;
        bool isCorrect;
        do
        {
            ColorDisplay(inputMessage, ConsoleColor.Green);
            isCorrect = char.TryParse(Console.ReadLine(), out result) && condition(result);
            if (!isCorrect)
                ColorDisplay(errorMessage, ConsoleColor.Red);
        } while (!isCorrect);
        return result;
    }
    //цветной вывод текста в консоль
    public static void ColorDisplay(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}

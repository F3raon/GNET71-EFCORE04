using System.Globalization;

namespace GNET71_EFCORE04.Services;

public static class InputService
{
    public static string ReadRequiredString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("This value is required.");
        }
    }

    public static string ReadOptionalString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var value))
            {
                return value;
            }

            Console.WriteLine("Please enter a valid whole number.");
        }
    }

    public static T ReadEnumChoice<T>(string prompt) where T : struct, Enum
    {
        var values = Enum.GetValues<T>();
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var number) && Enum.IsDefined(typeof(T), number))
            {
                return (T)Enum.ToObject(typeof(T), number);
            }

            Console.WriteLine($"Please choose a valid {typeof(T).Name}.");
            for (var index = 0; index < values.Length; index++)
            {
                Console.WriteLine($"{index + 1}) {values[index]}");
            }
        }
    }

    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
            {
                return value;
            }

            Console.WriteLine("Please enter a valid date, for example 2000-01-31.");
        }
    }
}

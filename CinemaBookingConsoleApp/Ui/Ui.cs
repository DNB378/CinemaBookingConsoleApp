using System.Globalization;

namespace CinemaBookingConsoleApp
{
    static class Ui
    {
        public static void Pause(string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Console.WriteLine("\n" + message);

            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }

        public static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s))
                    return s.Trim();

                Console.WriteLine("Поле не може бути порожнім.");
            }
        }

        public static string ReadOptional(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out var v) && v >= min && v <= max)
                    return v;

                Console.WriteLine($"Введіть ціле число в діапазоні {min}..{max}.");
            }
        }

        public static DateTime ReadDateTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(s))
                {
                    Console.WriteLine("Дата/час не може бути порожньою.");
                    continue;
                }

                if (DateTime.TryParseExact(s, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                    return dt;

                Console.WriteLine("Невірний формат. Приклад: 15.01.2026 18:30");
            }
        }
    }
}

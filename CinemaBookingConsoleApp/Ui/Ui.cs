using System.Globalization;

namespace CinemaBookingConsoleApp
{
    static class Ui
    {
        private static void WriteColored(string text, ConsoleColor color, bool newLine)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (newLine)
                Console.WriteLine(text);
            else
                Console.Write(text);
            Console.ForegroundColor = prev;
        }

        public static void WriteHeader(string text)
        {
            WriteColored(text, ConsoleColor.Cyan, true);
        }

        public static void WriteHint(string text)
        {
            WriteColored(text, ConsoleColor.DarkGray, true);
        }

        public static void WritePrompt(string text)
        {
            WriteColored(text, ConsoleColor.Green, false);
        }

        public static void WriteError(string text)
        {
            WriteColored(text, ConsoleColor.Red, true);
        }

        public static void WriteWarning(string text)
        {
            WriteColored(text, ConsoleColor.DarkYellow, true);
        }

        public static void Pause(string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine();
                WriteWarning(message);
            }

            Console.WriteLine();
            WriteHint("Натисніть Enter для продовження...");
            Console.ReadLine();
        }

        public static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                WritePrompt(prompt);
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s))
                    return s.Trim();

                WriteError("Поле не може бути порожнім.");
            }
        }

        public static string ReadOptional(string prompt)
        {
            WritePrompt(prompt);
            return Console.ReadLine() ?? "";
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                WritePrompt(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out var v) && v >= min && v <= max)
                    return v;

                WriteError($"Введіть ціле число в діапазоні {min}..{max}.");
            }
        }

        public static DateTime ReadDateTime(string prompt)
        {
            while (true)
            {
                WritePrompt(prompt);
                var s = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(s))
                {
                    WriteError("Дата/час не може бути порожньою.");
                    continue;
                }

                if (DateTime.TryParseExact(s, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                    return dt;

                WriteError("Невірний формат. Приклад: 15.01.2026 18:30");
            }
        }
    }
}

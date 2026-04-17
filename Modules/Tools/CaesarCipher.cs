using System;
using hub.demon.UI;

namespace hub.demon.Modules.Tools
{
    internal class CaesarCipher
    {
        public static void CaesarMain()
        {
            while (true)
            {
                int? choice = ConsoleNavigator.Navigation(
                    "CAESAR CIPHER",
                    new string[]
                    {
                        "Encrypt / Decrypt",
                        "Show all shifts (0–25)",
                        "Exit"
                    });

                if (choice == null || choice == 2)
                    return;

                if (choice == 0)
                {
                    RunCipherFlow();
                }
                else if (choice == 1)
                {
                    Console.Clear();
                    string message = GetMessage();
                    ShowAllShifts(message);
                }
            }
        }

        private static void RunCipherFlow()
        {
            Console.Clear();
            string message = GetMessage();

            while (true)
            {
                Console.Clear();
                int shift = GetShift();
                int direction = GetDirection(); // +1 or -1

                string result = Cipher(message, shift * direction);

                Console.Clear();
                Console.WriteLine("Result:\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(result);
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                // After result → ask what to do next
                int? next = ConsoleNavigator.Navigation(
                    "WHAT NEXT?",
                    new string[]
                    {
                        "Try again",
                        "Use result as new message",
                        "Show all shifts",
                        "Back to menu"
                    });

                if (next == null || next == 3)
                    return;

                if (next == 1)
                {
                    message = result;
                    continue;
                }

                if (next == 2)
                {
                    ShowAllShifts(message);
                }
            }
        }

        // --- INPUT HELPERS ---

        private static string GetMessage()
        {
            while (true)
            {
                Console.Write("Enter message: ");
                string input = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("\nThou cannot shift what is not there...");
                Pause();
            }
        }

        private static int GetShift()
        {
            while (true)
            {
                Console.Write("\nShift amount: ");
                if (int.TryParse(Console.ReadLine(), out int value) && value != 0)
                    return value;

                Console.WriteLine("\nThou cannot shift 0 times...");
                Pause();
            }
        }

        private static int GetDirection()
        {
            while (true)
            {
                Console.Write("\nDirection (left/right): ");
                string input = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input == "left" || input == "l") return -1;
                if (input == "right" || input == "r") return 1;

                Console.WriteLine("\nOnly 'left' or 'right' are allowed...");
                Pause();
            }
        }

        private static void Pause()
        {
            Console.WriteLine("Press any key to try again...");
            Console.ReadKey();
        }

        // --- CORE LOGIC ---

        private static string Cipher(string message, int shift)
        {
            char ShiftChar(char c)
            {
                if (!char.IsLetter(c))
                    return c;

                bool isUpper = char.IsUpper(c);
                char baseChar = isUpper ? 'A' : 'a';

                int offset = (c - baseChar + shift) % 26;
                if (offset < 0) offset += 26;

                return (char)(baseChar + offset);
            }

            char[] result = new char[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                result[i] = ShiftChar(message[i]);
            }

            return new string(result);
        }

        private static void ShowAllShifts(string message)
        {
            int selected = 0;

            while (true)
            {
                Console.Clear();

                int width = Console.WindowWidth;
                int height = Console.WindowHeight;

                string header = "All shifts (0–25) — Enter/Esc to go back";

                Console.SetCursorPosition(
                    Math.Max(0, (width - header.Length) / 2),
                    height / 2 - 4
                );
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(header);
                Console.ResetColor();

                int centerY = height / 2;

                for (int i = -2; i <= 2; i++)
                {
                    int shiftIndex = (selected + i + 26) % 26;

                    string text = $"{shiftIndex:00}: {Cipher(message, shiftIndex)}";

                    if (i == 0)
                        text = $">>> {text} <<<";

                    int y = centerY + i;

                    Console.SetCursorPosition(
                        Math.Max(0, (width - text.Length) / 2),
                        y
                    );

                    if (i == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (Math.Abs(i) == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Console.Write(text);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Escape:
                    case ConsoleKey.Enter:
                        return;

                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        selected = (selected - 1 + 26) % 26;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        selected = (selected + 1) % 26;
                        break;
                }
            }
        }
    }
}
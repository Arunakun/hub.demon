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
                int? choice = ConsoleHelper.Navigation(
                    "CAESAR CIPHER",
                    new string[]
                    {
                        "Use tool",
                        "Exit"
                    });

                if (choice == null || choice == 1)
                    return;

                if (choice == 0)
                    RunCipherFlow();
            }
        }

        private static void RunCipherFlow()
        {
            while (true)
            {
                Console.Clear();

                string message = GetMessage();
                int shift = GetShift();
                int direction = GetDirection(); // +1 or -1

                string result = Cipher(message, shift * direction);

                Console.Clear();
                Console.WriteLine("Result:\n");
                Console.WriteLine(result);
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                // After result → ask what to do next
                int? next = ConsoleHelper.Navigation(
                    "WHAT NEXT?",
                    new string[]
                    {
                        "Try again",
                        "Back to menu"
                    });

                if (next == null || next == 1)
                    return;
            }
        }

        // --- INPUT HELPERS ---

        private static string GetMessage()
        {
            while (true)
            {
                Console.Write("Enter message: ");
                string input = Console.ReadLine()?.ToLower() ?? "";

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("\nThou cannot shift what is not there...");
                Pause();
                Console.Clear();
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
                Console.Clear();
            }
        }

        private static int GetDirection()
        {
            while (true)
            {
                Console.Write("\nDirection (left/right): ");
                string input = Console.ReadLine()?.ToLower() ?? "";

                if (input == "left") return -1;
                if (input == "right") return 1;

                Console.WriteLine("\nOnly 'left' or 'right' are allowed...");
                Pause();
                Console.Clear();
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
                if (c < 'a' || c > 'z')
                    return c; // keep non-letters

                int offset = (c - 'a' + shift) % 26;
                if (offset < 0) offset += 26;

                return (char)('a' + offset);
            }

            char[] result = new char[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                result[i] = ShiftChar(message[i]);
            }

            return new string(result);
        }
    }
}
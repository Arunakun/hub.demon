using System;

namespace hub.demon.UI
{
    internal class ConsoleHelper
    {
        public static int? Navigation(string title, string[] options)
        {
            if (options == null || options.Length == 0)
                return null;

            int selected = 0;
            Console.CursorVisible = false;

            int visibleRange = 2;
            int boxWidth = 60; // 🔧 tweak this to your liking

            while (true)
            {
                Console.Clear();

                int width = Console.WindowWidth;
                int height = Console.WindowHeight;

                int boxX = (width - boxWidth) / 2;
                int boxTop = height / 2 - (visibleRange + 4);
                int boxBottom = height / 2 + (visibleRange + 4);

                int innerWidth = boxWidth - 4; // account for "||  ||"

                // --- Top border ---
                Console.SetCursorPosition(boxX, boxTop);
                Console.Write("+" + new string('=', boxWidth - 2) + "+");

                // --- Title ---
                Console.SetCursorPosition(boxX, boxTop + 1);
                Console.Write("||" + CenterText(title, boxWidth - 4) + "||");

                // --- Divider ---
                Console.SetCursorPosition(boxX, boxTop + 2);
                Console.Write("+" + new string('=', boxWidth - 2) + "+");

                // --- INTERIOR (REPLACES YOUR OLD FOR-LOOP) ---
                for (int y = boxTop + 3; y < boxBottom; y++)
                {
                    Console.SetCursorPosition(boxX, y);
                    Console.Write("||");

                    string content = new string(' ', innerWidth);

                    int centerY = (boxTop + boxBottom) / 2;
                    int relativeIndex = y - centerY;
                    int optionIndex = selected + relativeIndex;

                    if (Math.Abs(relativeIndex) <= visibleRange &&
                        optionIndex >= 0 &&
                        optionIndex < options.Length)
                    {
                        string text = options[optionIndex];

                        if (relativeIndex == 0)
                        {
                            text = $">>> {text} <<<";
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if (Math.Abs(relativeIndex) == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }

                        content = CenterText(text, innerWidth);
                    }

                    // Set color ONLY for content
                    if (relativeIndex == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (Math.Abs(relativeIndex) == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    // Write content
                    Console.Write(content);

                    // Reset BEFORE right border
                    Console.ResetColor();

                    // Right border (default color again)
                    Console.Write("||");
                }

                // --- Bottom border ---
                Console.SetCursorPosition(boxX, boxBottom);
                Console.Write("+" + new string('=', boxWidth - 2) + "+");

                // --- Input ---
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Escape:
                        return null;

                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        selected = (selected - 1 + options.Length) % options.Length;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        selected = (selected + 1) % options.Length;
                        break;

                    case ConsoleKey.Enter:
                        return selected;
                }
            }
        }

        private static string CenterText(string text, int width)
        {
            if (text.Length >= width)
                return text.Substring(0, width);

            int padding = width - text.Length;
            int padLeft = padding / 2;
            int padRight = padding - padLeft;

            return new string(' ', padLeft) + text + new string(' ', padRight);
        }
    }
}
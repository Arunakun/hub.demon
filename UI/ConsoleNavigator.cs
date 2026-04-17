namespace hub.demon.UI
{
    internal class ConsoleNavigator
    {
        private const int VisibleRange = 2;
        private const int BoxWidth = 60; // 🔧 tweak this to your liking
     
        // Displays an interactive console menu with keyboard navigation.
        // Returns selected index, or null if user cancels (ESC).
        public static int? Navigation(string title, string[] options)
        {
            if (options == null || options.Length == 0)
                return null;

            int selected = 0;
            Console.CursorVisible = false;

            while (true)
            {
                DrawMenu(title, options, selected);

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

        private static void DrawMenu(string title, string[] options, int selected)
        {
            Console.Clear();

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            int boxX = Math.Max(0, (width - BoxWidth) / 2);
            int boxTop = height / 2 - (VisibleRange + 4);
            int boxBottom = height / 2 + (VisibleRange + 4);

            int innerWidth = BoxWidth - 4;

            Console.SetCursorPosition(boxX, boxTop);
            Console.Write("+" + new string('=', BoxWidth - 2) + "+");

            Console.SetCursorPosition(boxX, boxTop + 1);
            Console.Write("||" + CenterText(title, BoxWidth - 4) + "||");

            Console.SetCursorPosition(boxX, boxTop + 2);
            Console.Write("+" + new string('=', BoxWidth - 2) + "+");

            for (int y = boxTop + 3; y < boxBottom; y++)
            {
                Console.SetCursorPosition(boxX, y);
                Console.Write("||");

                string content = new string(' ', innerWidth);

                int centerY = (boxTop + boxBottom) / 2;
                int relativeIndex = y - centerY;
                int optionIndex = selected + relativeIndex;

                if (Math.Abs(relativeIndex) <= VisibleRange &&
                    optionIndex >= 0 &&
                    optionIndex < options.Length)
                {
                    string text = options[optionIndex];

                    if (relativeIndex == 0)
                        text = $">>> {text} <<<";

                    content = CenterText(text, innerWidth);
                }

                if (relativeIndex == 0)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                else if (Math.Abs(relativeIndex) == 1)
                    Console.ForegroundColor = ConsoleColor.Gray;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(content);
                Console.ResetColor();
                Console.Write("||");
            }

            Console.SetCursorPosition(boxX, boxBottom);
            Console.Write("+" + new string('=', BoxWidth - 2) + "+");
        }

        private static string CenterText(string text, int width)
        {
            if (text.Length > width)
                return text.Substring(0, width - 3) + "...";

            int padding = width - text.Length;
            int padLeft = padding / 2;
            int padRight = padding - padLeft;

            return new string(' ', padLeft) + text + new string(' ', padRight);
        }
    }
}
using System.Text;
using hub.demon.UI;

namespace hub.demon.Modules.Tools
{
    internal class Base64EncoderDecoder
    {
        public static void Base64EncoderDecoderMain()
        {
            while (true)
            {
                int? choice = ConsoleNavigator.Navigation(
                    "BASE64 ENCODER/DECODER",
                    new string[]
                    {
                        "Encode",
                        "Decode",
                        "Exit"
                    });
                if (choice == null || choice == 2)
                    return;
                if (choice == 0)
                    RunBase64Flow(true);
                else if (choice == 1)
                    RunBase64Flow(false);
            }
        }

        public static void RunBase64Flow(bool startEncode)
        {
            string input = "";
            bool encodeMode = startEncode;

            while (true)
            {
                Console.Clear();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine(encodeMode
                        ? "Enter the text to encode:"
                        : "Enter the Base64 string to decode:");

                    input = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("\nInput cannot be empty.");
                        Console.ReadKey();
                        continue;
                    }
                }

                string result;

                try
                {
                    if (encodeMode)
                    {
                        result = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
                    }
                    else
                    {
                        byte[] data = Convert.FromBase64String(input);
                        result = Encoding.UTF8.GetString(data);
                    }

                    Console.Clear();

                    Console.WriteLine(encodeMode ? "Encoded result:\n" : "Decoded result:\n");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result);
                    Console.ResetColor();
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid Base64 string.");
                    Console.ReadKey();
                    input = ""; // force re-entry
                    continue;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                int? next = ConsoleNavigator.Navigation(
                    "WHAT NEXT?",
                    new string[]
                    {
                "Encode",
                "Decode",
                "Use result as new input",
                "Back to menu"
                    });

                if (next == null || next == 3)
                    return;

                if (next == 0)
                {
                    encodeMode = true;
                    input = ""; // force new input
                }
                else if (next == 1)
                {
                    encodeMode = false;
                    input = ""; // force new input
                }
                else if (next == 2)
                {
                    input = result; // reuse result
                }
            }
        }
    }
}
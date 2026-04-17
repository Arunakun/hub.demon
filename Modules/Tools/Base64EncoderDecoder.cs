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
                    RunEncoderFlow();
                if (choice == 1)
                    RunDecoderFlow();
            }
        }

        public static void RunEncoderFlow()
        {
            Console.Clear();
            Console.WriteLine("Enter the text to encode:");
            string input = Console.ReadLine() ?? "";
            string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
            Console.Clear();
            Console.WriteLine("Encoded result:\n");
            Console.WriteLine(encoded);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void RunDecoderFlow()
        {
            Console.Clear();
            Console.WriteLine("Enter the Base64 string to decode:");
            string input = Console.ReadLine() ?? "";
            try
            {
                byte[] data = Convert.FromBase64String(input);
                string decoded = Encoding.UTF8.GetString(data);
                Console.Clear();
                Console.WriteLine("Decoded result:\n");
                Console.WriteLine(decoded);
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Invalid Base64 string.");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
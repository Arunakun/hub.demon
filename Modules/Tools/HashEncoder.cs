using hub.demon.UI;
using System.Text;
using System.Security.Cryptography;

namespace hub.demon.Modules.Tools
{
    internal class HashEncoder
    {
        public static void HashEncoderMain()
        {
            int? choice = ConsoleNavigator.Navigation(
                "HASH TOOL",
                new string[]
                {
                    "MD5",
                    "SHA1",
                    "SHA256",
                    "SHA512",
                    "Back"
                });

            if (choice == null || choice == 4)
                return;

            RunEncoderFlow(choice.Value);
        }

        public static void RunEncoderFlow(int type)
        {
            string algoName = type switch
            {
                0 => "MD5",
                1 => "SHA1",
                2 => "SHA256",
                3 => "SHA512",
                _ => "Unknown"
            };

            while (true)
            {
                using HashAlgorithm algorithm = type switch
                {
                    0 => MD5.Create(),
                    1 => SHA1.Create(),
                    2 => SHA256.Create(),
                    3 => SHA512.Create(),
                    _ => throw new Exception("Invalid type")
                };

                Console.Clear();
                Console.WriteLine($"Enter the text to hash ({algoName}):");

                string input = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("\nInput cannot be empty.");
                    Console.ReadKey();
                    continue;
                }

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = algorithm.ComputeHash(inputBytes);

                string hashResult = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                Console.Clear();

                Console.WriteLine($"{algoName} Hash result:\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(hashResult);
                Console.ResetColor();

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                int? next = ConsoleNavigator.Navigation(
                    $"{algoName} COMPLETE",
                    new string[]
                    {
                        $"Hash again ({algoName})",
                        "Choose different algorithm",
                        "Back to menu"
                    });

                if (next == null || next == 2)
                    return;

                if (next == 1)
                {
                    HashEncoderMain(); // go back to algorithm selection
                    return;
                }

                // next == 0 → continue loop (same algorithm)
            }
        }
    }
}
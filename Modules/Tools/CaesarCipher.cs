using System.Numerics;

namespace hub.demon.Modules.Tools
{
    internal class CaesarCipher
    {
        public static void CaesarMain()
        {
            Console.Clear();
            Console.WriteLine("~~~~~~ caesar.cipher ~~~~~~\n");
            
            while (true)
            {
                Console.WriteLine("Press '1' to use this tool");
                Console.WriteLine("Press '2' to exit");
                Int32.TryParse(Console.ReadLine(), out int choice);
                Console.Clear();

                if (choice == 1)
                {
                    MessagePrep();
                }

                else if (choice == 2)
                {
                    Console.WriteLine("Thank you for using caesar.cipher");
                    Console.WriteLine("Press Enter to return to the hub...");
                    Console.ReadLine();
                    break;
                }

                else
                {
                    Console.WriteLine("Your input was invalid, please try again\n");
                }
            }
        }

        public static void MessagePrep()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter message to encrypt/decrypt: ");
                string userInput = Console.ReadLine()?.ToLower() ?? "";

                if (userInput == "")
                {
                    Console.WriteLine("\nThou cannot shift what is not there...");
                    Console.WriteLine("Press Enter to start over...");
                    Console.ReadLine();
                    continue;
                }

                Console.Write("\nEnter the amount of times the letters are to shift: ");
                Int32.TryParse(Console.ReadLine(), out int value);

                if (value == 0)
                {
                    Console.WriteLine("\nThou cannot shift 0 times...");
                    Console.WriteLine("Press Enter to start over...");
                    Console.ReadLine();
                    continue;
                }

                Console.Write("\nEnter direction of shifts (left/right): ");
                string dirInput = Console.ReadLine()?.ToLower() ?? "";

                if (dirInput == "")
                {
                    Console.WriteLine("\nThou cannot shift without choosing a direction...");
                    Console.WriteLine("Press Enter to start over...");
                    Console.ReadLine();
                    continue;
                }
                else if (dirInput != "left" && dirInput != "right")
                {
                    Console.WriteLine("\nThou cannot shift other than 'left' or 'right'...");
                    Console.WriteLine("Press Enter to start over...");
                    Console.ReadLine();
                    continue;
                }

                Cipher(userInput, value, dirInput);
                break;
            }
        }

        public static void Cipher(string message, int value, string direction)
        {
            Console.Clear();
            if (direction == "left")
            {
                value = -value;
            }

            string result = "";

            foreach (char letter in message)
            {
                int code = (int)letter + value;
                if (code > (int)'z')
                {
                    code -= 26;
                }

                else if (code < (int)'a')
                {
                    code += 26;
                }

                result += (char)code;
            }

            Console.WriteLine($"Your message has been (de)ciphered:\n{result}");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
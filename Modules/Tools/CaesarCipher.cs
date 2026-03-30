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
                Int32.TryParse(char.ToString(Console.ReadLine()[0]), out int choice);
                Console.Clear();

                if (choice == 1)
                {
                    Cipher();
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

        public static void Cipher()
        {
            while (true)
            {
                Console.Write("Enter message to encrypt/decrypt: ");
                string userInput = Console.ReadLine()?.ToLower() ?? "";

                if (userInput == "")
                {
                    Console.WriteLine("Thou cannot shift what is not there...");
                    Console.WriteLine("Press Enter to start over...");
                    continue;
                }

                Console.Write("Enter the amount of times the letters are to shift: ");
                Int32.TryParse(Console.ReadLine(), out int value);

                if (value == 0)
                {
                    Console.WriteLine("Thou cannot shift 0 times...");
                    Console.WriteLine("Press Enter to start over...");
                    continue;
                }

                Console.Write("Enter direction of shifts (left/right)");
                string dirInput = Console.ReadLine()?.ToLower() ?? "";

                if (dirInput == "")
                {
                    Console.WriteLine("Thou cannot shift without choosing a direction...");
                    Console.WriteLine("Press Enter to start over...");
                    continue;
                }
                else if (dirInput != "left" && dirInput != "right")
                {
                    Console.WriteLine("Thou cannot shift other than 'left' or 'right'...");
                    Console.WriteLine("Press Enter to start over...");
                    continue;
                }

            }
        }
    }
}
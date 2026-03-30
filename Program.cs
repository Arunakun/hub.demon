using System.Diagnostics;

namespace hubdemon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Action> parseCommand = new Dictionary<string, Action>();
            parseCommand.Add("exit", Exit);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("~~~~~~ .hubdemon ~~~~~~\n");
                Console.Write("Give me a command: ");

                string userInput = Console.ReadLine() ?? "";

                if (parseCommand.TryGetValue(userInput.ToLower(), out Action method))
                {
                    method();
                }
                else
                {
                    Console.WriteLine("I scoured hell and cannot find any commands that resembles yours...");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }

            }
        }


        static void Exit()
        {
            Console.WriteLine("You are now exiting");
            Console.WriteLine("Press Enter to exit .hubdemon");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
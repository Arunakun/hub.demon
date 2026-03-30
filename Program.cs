using hub.demon.commands;

namespace hub.demon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Action> parseCommand = new Dictionary<string, Action>();
            parseCommand.Add("exit", Commands.Exit);
            parseCommand.Add("caesar", Commands.Caesar);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("~~~~~~ hub.demon ~~~~~~\n");
                Console.Write("Give me a command: ");

                // ?. only calls .ToLower() if value is not null to avoid crashing, ?? "" replaces null with empty string to avoid crashing
                string userInput = Console.ReadLine()?.ToLower() ?? "";

                if (parseCommand.TryGetValue(userInput, out Action method))
                {
                    Console.Clear();
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
    }
}
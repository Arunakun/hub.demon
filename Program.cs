using System.Diagnostics;

namespace hubdemon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Action> parseCommand = new Dictionary<string, Action>();
            parseCommand.Add("exit", Exit);
            parseCommand.Add("todo", ToDo);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("~~~~~~ .hubdemon ~~~~~~\n");
                Console.Write("Give me a command: ");

                if (parseCommand.TryGetValue(Console.ReadLine(), out Action method))
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

        static void ToDo()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "todo.exe",
                UseShellExecute = true
            });

            Environment.Exit(0);
        }
    }
}
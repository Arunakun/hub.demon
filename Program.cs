using hub.demon.commands;
using hub.demon.UI;

namespace hub.demon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] menuMain =
            {
                "Caesar Cipher",
                "Exit",
                "Test1",
                "Test2",
                "Test3",
                "Test4",
                "Test5"
            };

            while (true)
            {
                int? choice = ConsoleHelper.Navigation("HOVEDMENU", menuMain);

                if (choice == null)
                {
                    return;
                }

                switch (choice)
                {
                    case 0:
                        Console.Clear();
                        Commands.Caesar();
                        break;

                    case 1:
                        Console.Clear();
                        Commands.Exit();
                        break;
                }
            }
        }
    }
}
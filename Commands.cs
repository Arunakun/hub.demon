namespace hub.demon.commands
{
    internal static class Commands
    {
        public static void Exit()
        {
            Console.WriteLine("You are now exiting");
            Console.WriteLine("Press Enter to exit hub.demon");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
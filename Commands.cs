using hub.demon.Modules.Tools;

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

        public static void Caesar()
        {
            Console.WriteLine("Transferring to caesar.cipher");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            CaesarCipher.CaesarMain();
        }
    }
}
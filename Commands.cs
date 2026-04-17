using hub.demon.Modules.Tools;

namespace hub.demon.commands
{
    internal static class Commands
    {
        public static void Exit()
        {
            Console.WriteLine("You are now exiting");
            Console.WriteLine("Press any key to exit hub.demon");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static void Caesar()
        {
            Console.WriteLine("Transferring to caesar.cipher");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            CaesarCipher.CaesarMain();
        }
    }
}
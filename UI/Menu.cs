using hub.demon.UI;

namespace hub.demon
{
    internal class Menu
    {
        private List<(string Name, Action Action)> _items =
            new List<(string, Action)>
            {
                ("Caesar Cipher", Modules.Tools.CaesarCipher.CaesarMain),
                ("Base64 Encoder/Decoder", Modules.Tools.Base64EncoderDecoder.Base64EncoderDecoderMain)
            };

        public void Run()
        {
            while (true)
            {
                var options = _items.Select(i => i.Name).ToList();
                options.Add("Exit");

                int? choice = ConsoleHelper.Navigation("HOVEDMENU", options.ToArray());

                if (choice == null || choice == options.Count - 1)
                    return;

                _items[choice.Value].Action();
            }
        }
    }
}
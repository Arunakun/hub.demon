namespace hub.demon
{
    // Represents the main menu of the application, allowing users to select different tools.
    // Each item maps a display name to an action that executes the corresponding tool's main function.
    internal class Menu
    {
        private List<MenuItem> _items =
            new List<MenuItem>
            {
                new MenuItem("CyberSec Tools", new List<MenuItem>
                {
                    new MenuItem("Caesar Cipher", Modules.Tools.CaesarCipher.CaesarMain),
                    new MenuItem("Base64 Encoder/Decoder", Modules.Tools.Base64EncoderDecoder.Base64EncoderDecoderMain),
                    new MenuItem("Hash Encoder", Modules.Tools.HashEncoder.HashEncoderMain)
                }),

                new MenuItem("Minigames", new List<MenuItem>
                {
                    // empty for now
                })
            };

        public void Run()
        {
            ShowMenu("HOVEDMENU", _items, isRoot: true);
        }

        private void ShowMenu(string title, List<MenuItem> items, bool isRoot = false)
        {
            while (true)
            {
                var options = items.Select(i => i.Name).ToList();
                options.Add(isRoot ? "Exit" : "Back");

                int? choice = UI.ConsoleNavigator.Navigation(title, options.ToArray());

                int exitIndex = options.Count - 1;

                if (choice == null || choice == exitIndex)
                    return;

                var selectedItem = items[choice.Value];

                if (selectedItem.IsLeaf)
                {
                    selectedItem.Execute!();
                }
                else
                {
                    ShowMenu(selectedItem.Name, selectedItem.Children!, false);
                }
            }
        }
    }
}
using hub.demon.Modules.Tools.CyberSec;

namespace hub.demon
{
    // Represents the main menu of the application, allowing users to select different tools.
    // Each item maps a display name to an action that executes the corresponding tool's main function.
    internal class Menu
    {
        private List<MenuItem> _items = new List<MenuItem>
        {
            new MenuItem("Tools", new List<MenuItem>
            {
                new MenuItem("CyberSec", new List<MenuItem>
                {
                    new MenuItem("Caesar Cipher", CaesarCipher.CaesarMain),
                    new MenuItem("Base64 Encoder/Decoder", Base64EncoderDecoder.Base64EncoderDecoderMain),
                    new MenuItem("Hash Encoder", HashEncoder.HashEncoderMain)
                }),
        
                new MenuItem("Simulation", new List<MenuItem>
                {
                    new MenuItem("OSRS Loot Simulator", Modules.Tools.Simulation.OSRS.LootRunner.RunSimulator)
                })
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
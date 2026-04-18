namespace hub.demon.Modules.Tools.Simulation.OSRS
{
    internal class LootTable
    {
        public List<LootItem> Main { get; set; } = new();
        public List<LootItem> Tertiary { get; set; } = new();
    }
}
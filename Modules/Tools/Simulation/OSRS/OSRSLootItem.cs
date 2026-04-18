namespace hub.demon.Modules.Tools.Simulation.OSRS
{
    internal class LootItem
    {
        public string Name { get; set; } = "";
        public int MinQty { get; set; }
        public int MaxQty { get; set; }
        public double Probability { get; set; }
    }
}
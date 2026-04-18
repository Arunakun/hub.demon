using System;
using System.Collections.Generic;
using System.Linq;

namespace hub.demon.Modules.Tools.Simulation.OSRS
{
    internal static class LootSimulator
    {
        static Random rng = new Random();

        public static int RollUntilComplete(LootTable table)
        {
            var obtained = new HashSet<string>();
            var target = BuildTargetSet(table);

            int rolls = 0;

            while (!target.All(obtained.Contains))
            {
                rolls++;

                // MAIN
                var main = WeightedChoice(table.Main);
                int qty = main.Name == "Nothing"
                    ? 0
                    : rng.Next(main.MinQty, main.MaxQty + 1);

                obtained.Add(main.Name);

                // TERT
                foreach (var item in table.Tertiary)
                {
                    if (rng.NextDouble() <= item.Probability)
                    {
                        int tQty = rng.Next(item.MinQty, item.MaxQty + 1);
                        obtained.Add(item.Name);
                    }
                }
            }

            return rolls;
        }

        private static HashSet<string> BuildTargetSet(LootTable table)
        {
            var set = new HashSet<string>();

            foreach (var item in table.Main.Concat(table.Tertiary))
            {
                if (item.Name == "Nothing")
                    continue;

                set.Add(item.Name);
            }

            return set;
        }

        private static LootItem WeightedChoice(List<LootItem> items)
        {
            double total = items.Sum(i => i.Probability);
            double roll = rng.NextDouble() * total;

            double current = 0;

            foreach (var item in items)
            {
                current += item.Probability;
                if (roll <= current)
                    return item;
            }

            return items.Last();
        }
    }
}
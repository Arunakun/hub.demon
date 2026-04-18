using System;
using System.Collections.Generic;
using System.Linq;

namespace hub.demon.Modules.Tools.Simulation.OSRS
{
    internal static class LootSimulator
    {
        static Random rng = new Random();

        public static (int rolls, Dictionary<string, int> drops) RollUntilComplete(LootTable table)
        {
            var obtained = new HashSet<string>();
            var target = BuildTargetSet(table);
            var drops = new Dictionary<string, int>();

            int rolls = 0;

            while (!target.All(obtained.Contains))
            {
                rolls++;

                // MAIN
                var main = WeightedChoice(table.Main);
                int qty = main.Name == "Nothing"
                    ? 0
                    : rng.Next(main.MinQty, main.MaxQty + 1);

                if (main.Name != "Nothing")
                {
                    if (!drops.ContainsKey(main.Name))
                        drops[main.Name] = 0;

                    drops[main.Name] += qty;
                }

                string key = $"{main.MinQty}x {main.Name}";
                obtained.Add(key);

                // TERT
                foreach (var item in table.Tertiary)
                {
                    if (rng.NextDouble() <= item.Probability)
                    {
                        int tQty = rng.Next(item.MinQty, item.MaxQty + 1);

                        if (!drops.ContainsKey(item.Name))
                            drops[item.Name] = 0;

                        drops[item.Name] += tQty;

                        string key2 = $"{item.MinQty}x {item.Name}";
                        obtained.Add(key2);
                    }
                }
            }

            return (rolls, drops);
        }

        private static HashSet<string> BuildTargetSet(LootTable table)
        {
            var set = new HashSet<string>();

            foreach (var item in table.Main.Concat(table.Tertiary))
            {
                if (item.Name == "Nothing")
                    continue;

                string key = $"{item.MinQty}x {item.Name}";
                set.Add(key);
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

        public static Dictionary<string, int> RollWithTracking(LootTable table, int rolls)
        {
            var drops = new Dictionary<string, int>();

            for (int i = 0; i < rolls; i++)
            {
                // MAIN
                var main = WeightedChoice(table.Main);
                int qty = main.Name == "Nothing"
                    ? 0
                    : rng.Next(main.MinQty, main.MaxQty + 1);

                if (main.Name != "Nothing")
                {
                    if (!drops.ContainsKey(main.Name))
                        drops[main.Name] = 0;

                    string key = $"{main.MinQty}x {main.Name}";

                    if (!drops.ContainsKey(main.Name))
                        drops[main.Name] = 0;

                    drops[main.Name] += qty;
                }

                // TERT
                foreach (var item in table.Tertiary)
                {
                    if (rng.NextDouble() <= item.Probability)
                    {
                        int tQty = rng.Next(item.MinQty, item.MaxQty + 1);

                        if (!drops.ContainsKey(item.Name))
                            drops[item.Name] = 0;

                        string key = $"{item.MinQty}x {item.Name}";

                        if (!drops.ContainsKey(item.Name))
                            drops[item.Name] = 0;

                        drops[item.Name] += tQty;
                    }
                }
            }

            return drops;
        }
    }
}
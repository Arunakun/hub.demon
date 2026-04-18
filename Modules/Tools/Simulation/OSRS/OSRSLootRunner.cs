using hub.demon.UI;
using System.IO;
using System.Linq;

namespace hub.demon.Modules.Tools.Simulation.OSRS
{
    internal static class LootRunner
    {
        public static void RunSimulator()
        {
            while (true)
            {
                Console.Clear();

                string basePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    @"..\..\..\Modules\Tools\Simulation\OSRS\Monsters"
                );

                var files = Directory.GetFiles(basePath, "*.txt")
                                     .Select(f => Path.GetFileNameWithoutExtension(f))
                                     .ToList();

                files.Add("Back");

                int? choice = ConsoleNavigator.Navigation("SELECT MONSTER", files.ToArray());

                if (choice == null || choice == files.Count - 1)
                    return;

                string name = files[choice.Value];
                string fullPath = Path.Combine(basePath, $"{name}.txt");

                Console.Clear();
                Console.Write("How many simulations: ");
                if (!int.TryParse(Console.ReadLine(), out int runs) || runs <= 0)
                {
                    Console.WriteLine("\nInvalid number.");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    var table = LootParser.Parse(fullPath);

                    var results = RunSequence(table, runs);

                    Console.Clear();
                    Console.WriteLine($"Completed {name} {runs} times!\n");

                    Console.WriteLine($"Fastest: {results.fastest}");
                    Console.WriteLine($"Slowest: {results.slowest}");
                    Console.WriteLine($"Average: {results.average:F2}");
                    Console.WriteLine($"Median:  {results.median:F2}");

                    ShowLuckAnalysis(table, results.totalKills, results.totalDrops);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                int? next = ConsoleNavigator.Navigation(
                    "WHAT NEXT?",
                    new string[]
                    {
                        "Run again",
                        "Back to menu"
                    });

                if (next == null || next == 1)
                    return;
            }
        }

        private static (int fastest, int slowest, double average, double median, int totalKills, Dictionary<string, int> totalDrops) RunSequence(LootTable table, int runs)
        {
            var results = new List<int>();
            var totalDrops = new Dictionary<string, int>();

            for (int i = 0; i < runs; i++)
            {
                var (rolls, drops) = LootSimulator.RollUntilComplete(table);

                results.Add(rolls);

                foreach (var kvp in drops)
                {
                    if (!totalDrops.ContainsKey(kvp.Key))
                        totalDrops[kvp.Key] = 0;

                    totalDrops[kvp.Key] += kvp.Value;
                }
            }

            results.Sort();

            int fastest = results.First();
            int slowest = results.Last();
            double average = results.Average();

            double median;
            int count = results.Count;

            if (count % 2 == 1)
                median = results[count / 2];
            else
                median = (results[(count / 2) - 1] + results[count / 2]) / 2.0;

            int totalKills = results.Sum();

            return (fastest, slowest, average, median, totalKills, totalDrops);
        }

        private static void ShowLuckAnalysis(LootTable table, int rolls, Dictionary<string, int> drops)
        {
            var grouped = GroupByName(table);

            string? luckiest = null;
            double bestRatio = double.MinValue;

            string? unluckiest = null;
            double worstRatio = double.MaxValue;

            double bestExpected = 0;
            int bestActual = 0;

            double worstExpected = 0;
            int worstActual = 0;

            foreach (var kvp in grouped)
            {
                string name = kvp.Key;
                var items = kvp.Value;

                // --- EXPECTED ---
                double expected = 0;

                foreach (var item in items)
                {
                    double avgQty = (item.MinQty + item.MaxQty) / 2.0;
                    expected += rolls * item.Probability * avgQty;
                }

                // --- ACTUAL ---
                drops.TryGetValue(name, out int actual);

                if (expected <= 0)
                    continue;

                double ratio = actual / expected;

                if (ratio > bestRatio)
                {
                    bestRatio = ratio;
                    luckiest = name;
                    bestExpected = expected;
                    bestActual = actual;
                }

                if (ratio < worstRatio)
                {
                    worstRatio = ratio;
                    unluckiest = name;
                    worstExpected = expected;
                    worstActual = actual;
                }
            }

            Console.WriteLine($"\n--- Luck Analysis (based on {rolls} total kills) ---\n");

            // ⭐ Luckiest
            if (luckiest != null)
            {
                var items = grouped[luckiest];
                bool multiple = items.Count > 1;

                double avgRate = items.Average(i => 1 / i.Probability);

                Console.WriteLine("Luckiest item:");
                Console.WriteLine($"{luckiest} (1/{avgRate:F2}{(multiple ? " on average" : "")})");
                Console.WriteLine($"Expected: ~{bestExpected:F2} {luckiest.ToLower()}");
                Console.WriteLine($"Got: {bestActual} {luckiest.ToLower()}");
                Console.WriteLine($"{bestRatio:F2}x more than expected");

                if (bestRatio > 2)
                    Console.WriteLine("That's insanely lucky!");
                else if (bestRatio > 1.2)
                    Console.WriteLine("That's kinda lucky!");
                else
                    Console.WriteLine("Slightly lucky.");

                Console.WriteLine();
            }

            // 💀 Unluckiest
            if (unluckiest != null)
            {
                var items = grouped[unluckiest];
                bool multiple = items.Count > 1;

                double avgRate = items.Average(i => 1 / i.Probability);

                Console.WriteLine("Unluckiest item:");
                Console.WriteLine($"{unluckiest} (1/{avgRate:F2}{(multiple ? " on average" : "")})");
                Console.WriteLine($"Expected: ~{worstExpected:F2} {unluckiest.ToLower()}");
                Console.WriteLine($"Got: {worstActual} {unluckiest.ToLower()}");
                Console.WriteLine($"{worstRatio:F2}x of expected");

                if (worstRatio == 0)
                    Console.WriteLine("You never got it. Extremely unlucky.");
                else if (worstRatio < 0.5)
                    Console.WriteLine("Very unlucky.");
                else if (worstRatio < 0.9)
                    Console.WriteLine("Slightly unlucky.");

                Console.WriteLine();
            }
        }

        private static Dictionary<string, List<LootItem>> GroupByName(LootTable table)
        {
            return table.Main
                .Concat(table.Tertiary)
                .Where(i => i.Name != "Nothing")
                .GroupBy(i => i.Name)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
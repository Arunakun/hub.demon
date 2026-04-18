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

        private static (int fastest, int slowest, double average, double median) RunSequence(LootTable table, int runs)
        {
            var results = new List<int>();

            for (int i = 0; i < runs; i++)
            {
                results.Add(LootSimulator.RollUntilComplete(table));
            }

            results.Sort();

            int fastest = results.First();
            int slowest = results.Last();

            double average = results.Average();

            double median;
            int count = results.Count;

            if (count % 2 == 1)
            {
                median = results[count / 2];
            }
            else
            {
                median = (results[(count / 2) - 1] + results[count / 2]) / 2.0;
            }

            return (fastest, slowest, average, median);
        }
    }
}
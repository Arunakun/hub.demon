using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace hub.demon.Modules.Tools.Simulation.OSRS
{
    using System.Globalization;

    internal static class LootParser
    {
        public static LootTable Parse(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var table = new LootTable();
            List<LootItem>? currentList = null;

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("["))
                {
                    if (line.Contains("MAIN"))
                        currentList = table.Main;
                    else if (line.Contains("TERT"))
                        currentList = table.Tertiary;
                    else
                        currentList = null;

                    continue;
                }

                if (currentList == null)
                    continue;

                var parts = line.Split(',');

                if (parts.Length < 4)
                    continue;

                string name = parts[0].Trim();
                int min = string.IsNullOrWhiteSpace(parts[1]) ? 0 : int.Parse(parts[1]);
                int max = string.IsNullOrWhiteSpace(parts[2]) ? 0 : int.Parse(parts[2]);

                double prob = ParseProbability(parts[3]);

                currentList.Add(new LootItem
                {
                    Name = name,
                    MinQty = min,
                    MaxQty = max,
                    Probability = prob
                });
            }

            return table;
        }

        private static double ParseProbability(string input)
        {
            if (!input.Contains('/')) return 0;

            var parts = input.Split('/');
            double num = double.Parse(parts[0], CultureInfo.InvariantCulture);
            double den = double.Parse(parts[1], CultureInfo.InvariantCulture);

            return num / den;
        }
    }
}
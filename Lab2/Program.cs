using System;
using System.IO;
using System.Linq;

namespace Lab2
{
    public class Program
    {
        public static void Main()
        {
            // Path configuration moved to a method for easier testing
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.TXT");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.TXT");

            var result = CalculateCostFromFile(inputFilePath);
            File.WriteAllText(outputFilePath, result.ToString());
        }

        // Separate method for testing logic without file I/O
        public static int CalculateCostFromFile(string inputFilePath)
        {
            string[] lines = File.ReadAllLines(inputFilePath);
            string[] firstLine = lines[0].Split();
            int n = int.Parse(firstLine[0]);
            int m = int.Parse(firstLine[1]);

            (int, int)[] suppliers = new (int, int)[m];
            for (int i = 0; i < m; i++)
            {
                string[] input = lines[i + 1].Split();
                int ai = int.Parse(input[0]);
                int bi = int.Parse(input[1]);
                suppliers[i] = (ai, bi);
            }

            var sortedSuppliers = suppliers
                .Select(s => new { Pairs = s.Item1, Price = s.Item2, PricePerPair = (double)s.Item2 / s.Item1 })
                .OrderBy(s => s.PricePerPair)
                .ToArray();

            int totalPairs = 0;
            int totalCost = 0;

            foreach (var supplier in sortedSuppliers)
            {
                if (totalPairs >= n)
                    break;

                int neededPairs = n - totalPairs;
                int packsToBuy = Math.Min(neededPairs / supplier.Pairs + (neededPairs % supplier.Pairs == 0 ? 0 : 1), neededPairs);

                totalPairs += packsToBuy * supplier.Pairs;
                totalCost += packsToBuy * supplier.Price;
            }

            return totalCost;
        }
    }
}

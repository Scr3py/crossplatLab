namespace ClassLibraryLab4
{
    public class Lab2
    {
        public static void ExecuteLab2(string inputFile, string outputFile)
        {
            try
            {
                // Читаємо вхідні дані
                string inputData = ReadInputData(inputFile);

                // Розраховуємо вартість
                int totalCost = CalculateCostFromInput(inputData);

                // Записуємо результат у файл
                File.WriteAllText(outputFile, totalCost.ToString());

                Console.WriteLine($"Results have been successfully saved to {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static string ReadInputData(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException($"Input file not found: {inputFile}");
            }

            string inputData = File.ReadAllText(inputFile).Trim();
            if (string.IsNullOrEmpty(inputData))
            {
                throw new ArgumentNullException("Input data cannot be empty or null.");
            }
            return inputData;
        }

        private static int CalculateCostFromInput(string inputData)
        {
            string[] lines = inputData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] firstLine = lines[0].Split();
            int n = int.Parse(firstLine[0]);
            int m = int.Parse(firstLine[1]);

            (int Pairs, int Price)[] suppliers = new (int, int)[m];
            for (int i = 0; i < m; i++)
            {
                string[] supplierData = lines[i + 1].Split();
                int ai = int.Parse(supplierData[0]);
                int bi = int.Parse(supplierData[1]);
                suppliers[i] = (ai, bi);
            }

            var sortedSuppliers = suppliers
                .Select(s => new { s.Pairs, s.Price, PricePerPair = (double)s.Price / s.Pairs })
                .OrderBy(s => s.PricePerPair)
                .ToArray();

            int totalPairs = 0;
            int totalCost = 0;

            foreach (var supplier in sortedSuppliers)
            {
                if (totalPairs >= n)
                    break;

                int neededPairs = n - totalPairs;
                int packsToBuy = Math.Min((neededPairs + supplier.Pairs - 1) / supplier.Pairs, neededPairs);

                totalPairs += packsToBuy * supplier.Pairs;
                totalCost += packsToBuy * supplier.Price;
            }

            return totalCost;
        }
    }
}

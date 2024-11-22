namespace ClassLibraryLab4
{
    public class Lab1
    {
        public static void ExecuteLab1(string inputFile, string outputFile)
        {
            try
            {
                string input = ReadInputData(inputFile);

                if (!int.TryParse(input, out int N) || N <= 0)
                {
                    throw new ArgumentException("Input should be a positive integer.");
                }

                int result = GenerateSequence(N);

                File.WriteAllText(outputFile, result.ToString());

                Console.WriteLine($"Result has been successfully saved to {outputFile}");
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

            string input = File.ReadAllText(inputFile).Trim();
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("Input string cannot be empty or null.");
            }
            return input;
        }

        public static bool IsPrime(int number)
        {
            if (number < 2) return false; 
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false; 
            }
            return true;
        }

   
        public static int GenerateSequence(int N)
        {
            int count = 0; 
            int current = 7; 

            while (true)
            {
                if (IsPrime(current) || current % 7 == 0)
                {
                    count++;
                    if (count == N)
                    {
                        return current; 
                    }
                }
                current++;
            }

        }
    }
}

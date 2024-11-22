namespace ClassLibraryLab5
{
    public class LibLab1
    {
        public static string ExecuteLab1(string input)
        {
            try
            {
                if (!int.TryParse(input, out int N) || N <= 0)
                {
                    throw new ArgumentException("Input should be a positive integer.");
                }

                int result = GenerateSequence(N);

                return $"The {N}th prime or multiple of 7 is: {result}";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
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

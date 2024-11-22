using System;
using System.IO;

namespace Lab1;
public class Program
{
    static void Main()
    {
        // Зчитуємо вхідний файл
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "INPUT.TXT");
        int N = int.Parse(File.ReadAllLines(inputPath)[0]); // Отримуємо перше число з файлу

        // Генеруємо послідовність до N-го елемента
        int result = GenerateSequence(N);

        // Записуємо результат у вихідний файл
        string outputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "OUTPUT.TXT");
        File.WriteAllText(outputPath, result.ToString());
    }

    // Метод для перевірки, чи є число простим
    static bool IsPrime(int number)
    {
        if (number < 2) return false; // 1 не є простим
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0) return false; // Якщо має дільник, то не просте
        }
        return true;
    }

    // Метод для генерації N-го члена послідовності
    public static int GenerateSequence(int N)
    {
        int count = 0; // Лічильник чисел у послідовності
        int current = 7; // Починаємо з першого числа послідовності

        while (true)
        {
            if (IsPrime(current) || current % 7 == 0)
            {
                count++;
                if (count == N)
                {
                    return current; // Повертаємо N-те число
                }
            }
            current++;
        }
    }
}

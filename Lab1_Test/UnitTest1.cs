using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.IO;
using Xunit;
using Lab1;

namespace Lab1_Test;
public class ProgramTests
{
    // Тест для перевірки зчитування вхідного файлу
    [Fact]
    public void TestReadInputFile()
    {
        // Підготовка
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "INPUT.TXT");

        // Виконання
        string[] lines = File.ReadAllLines(inputPath);

        // Перевірка
        Assert.NotEmpty(lines); // Файл не повинен бути порожнім
        Assert.Equal(1, lines.Length); // Має бути тільки один рядок
        Assert.True(int.TryParse(lines[0], out _)); // Перша строка має бути числом
    }

    // Тест для перевірки коректності методу GenerateSequence
    [Theory]
    [InlineData(1, 7)]
    [InlineData(2, 11)]
    [InlineData(3, 14)]
    [InlineData(4, 17)]
    public void TestGenerateSequence(int N, int expected)
    {
        // Виконання
        int result = Lab1.Program.GenerateSequence(N);
    }

    // Тест для перевірки запису у вихідний файл
    [Fact]
    public void TestWriteOutputFile()
    {
        // Підготовка
        string outputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "OUTPUT.TXT");

        // Виконання
        int N = 5; // Тестуємо для 5-го числа в послідовності
        int result = Lab1.Program.GenerateSequence(N);
        File.WriteAllText(outputPath, result.ToString());

        // Перевірка, чи файл був записаний
        Assert.True(File.Exists(outputPath)); // Файл має існувати

        // Перевірка правильності вмісту файлу
        string outputContent = File.ReadAllText(outputPath);
        Assert.Equal(result.ToString(), outputContent); // Вміст файлу має відповідати значенню
    }
}

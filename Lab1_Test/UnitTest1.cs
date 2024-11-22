using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.IO;
using Xunit;
using Lab1;

namespace Lab1_Test;
public class ProgramTests
{
    // ���� ��� �������� ���������� �������� �����
    [Fact]
    public void TestReadInputFile()
    {
        // ϳ��������
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "INPUT.TXT");

        // ���������
        string[] lines = File.ReadAllLines(inputPath);

        // ��������
        Assert.NotEmpty(lines); // ���� �� ������� ���� �������
        Assert.Equal(1, lines.Length); // �� ���� ����� ���� �����
        Assert.True(int.TryParse(lines[0], out _)); // ����� ������ �� ���� ������
    }

    // ���� ��� �������� ���������� ������ GenerateSequence
    [Theory]
    [InlineData(1, 7)]
    [InlineData(2, 11)]
    [InlineData(3, 14)]
    [InlineData(4, 17)]
    public void TestGenerateSequence(int N, int expected)
    {
        // ���������
        int result = Lab1.Program.GenerateSequence(N);
    }

    // ���� ��� �������� ������ � �������� ����
    [Fact]
    public void TestWriteOutputFile()
    {
        // ϳ��������
        string outputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "OUTPUT.TXT");

        // ���������
        int N = 5; // ������� ��� 5-�� ����� � �����������
        int result = Lab1.Program.GenerateSequence(N);
        File.WriteAllText(outputPath, result.ToString());

        // ��������, �� ���� ��� ���������
        Assert.True(File.Exists(outputPath)); // ���� �� ��������

        // �������� ����������� ����� �����
        string outputContent = File.ReadAllText(outputPath);
        Assert.Equal(result.ToString(), outputContent); // ���� ����� �� ��������� ��������
    }
}

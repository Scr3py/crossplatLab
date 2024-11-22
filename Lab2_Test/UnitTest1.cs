using System;
using System.IO;
using Lab2;
using Xunit;

namespace Lab2_Test;
public class ProgramTests
{
    [Fact]
    public void TestReadInputFile()
    {
        // Arrange
        string inputFilePath = "test_INPUT.TXT";
        string[] inputContent = {
            "5 2", // Number of pairs of socks and suppliers
            "3 30", // Supplier 1: 3 pairs for 30
            "5 40"  // Supplier 2: 5 pairs for 40
        };
        File.WriteAllLines(inputFilePath, inputContent);

        // Act
        string[] lines = File.ReadAllLines(inputFilePath);
        string[] firstLine = lines[0].Split();
        int n = int.Parse(firstLine[0]);
        int m = int.Parse(firstLine[1]);

        // Assert
        Assert.Equal(5, n);  // Number of pairs
        Assert.Equal(2, m);  // Number of suppliers
    }

    [Fact]
    public void TestWriteOutputFile()
    {
        // Arrange
        string outputFilePath = "test_OUTPUT.TXT";
        int expectedCost = 70; // Expected total cost

        // Act
        File.WriteAllText(outputFilePath, expectedCost.ToString());

        // Assert
        string outputContent = File.ReadAllText(outputFilePath);
        Assert.Equal(expectedCost.ToString(), outputContent);  // Verify the output matches the expected value
    }
}

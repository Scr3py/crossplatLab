using System;
using System.IO;
using Xunit;
using Lab3;

namespace Lab3_Test
{
    public class ProgramTests
    {
        [Fact]
        public void Test_Input1_ShouldReturnExpectedOutput()
        {
            // Arrange
            string input = "3 3\n1 2 3 4\nS.X\n.R.\n.E.\n";
            string expectedOutput = "0"; // Припустимо, це правильний результат

            // Redirecting input and output
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");

            File.WriteAllText(inputFilePath, input);

            // Act
            Lab3.Program.Main();

            string actualOutput = File.ReadAllText(outputFilePath);

            // Assert
            Assert.Equal(expectedOutput, actualOutput.Trim());
        }

        [Fact]
        public void Test_Input2_NoValidPath_ShouldReturnSleep()
        {
            // Arrange
            string input = "3 3\n1 2 3 4\nSXX\nXRX\nXXE\n";
            string expectedOutput = "Sleep";

            // Redirecting input and output
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");

            File.WriteAllText(inputFilePath, input);

            // Act
            Program.Main();

            string actualOutput = File.ReadAllText(outputFilePath);

            // Assert
            Assert.Equal(expectedOutput, actualOutput.Trim());
        }

        [Fact]
        public void Test_Input3_WithKeys_ShouldReturnMinimumCost()
        {
            // Arrange
            string input = "4 4\n10 20 30 40\nS...\n.XR.\n.GYE\n..E.\n";
            string expectedOutput = "0"; // З урахуванням вартості ключів і руху

            // Redirecting input and output
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");

            File.WriteAllText(inputFilePath, input);

            // Act
            Program.Main();

            string actualOutput = File.ReadAllText(outputFilePath);

            // Assert
            Assert.Equal(expectedOutput, actualOutput.Trim());
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace Lab3;
public class Program
{
    static char[,] maze;
    static int R, C;
    static int[] keyCosts = new int[4];  // Р вартості ключів: R, G, B, Y
    static (int, int) start, end;

    static int[] dRow = { -1, 1, 0, 0 };  // Напрями руху: вгору, вниз, ліво, право
    static int[] dCol = { 0, 0, -1, 1 };

    public static void Main()
    {
        // Вказуємо шляхи до файлів INPUT.TXT і OUTPUT.TXT
        string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt");
        string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt");

        // Читання вхідних даних з файлу
        string[] inputLines = File.ReadAllLines(inputFilePath);
        var dimensions = inputLines[0].Split();
        R = int.Parse(dimensions[0]);
        C = int.Parse(dimensions[1]);

        keyCosts = Array.ConvertAll(inputLines[1].Split(), int.Parse);

        maze = new char[R, C];
        for (int i = 0; i < R; i++)
        {
            string row = inputLines[i + 2];
            for (int j = 0; j < C; j++)
            {
                maze[i, j] = row[j];
                if (row[j] == 'S') start = (i, j);
                if (row[j] == 'E') end = (i, j);
            }
        }

        // Запуск BFS
        var result = BFS();

        // Запис результату в OUTPUT.TXT
        File.WriteAllText(outputFilePath, result);
    }

    static string BFS()
    {
        var queue = new Queue<(int row, int col, int keyMask, int cost)>();
        bool[,,] visited = new bool[R, C, 16];  // Відвідані клітини з різними комбінаціями ключів (16 = 2^4)

        queue.Enqueue((start.Item1, start.Item2, 0, 0));  // Початкова позиція без ключів і витрат
        visited[start.Item1, start.Item2, 0] = true;

        while (queue.Count > 0)
        {
            var (row, col, keyMask, cost) = queue.Dequeue();

            // Якщо досягли виходу, повертаємо результат
            if ((row, col) == end)
            {
                return cost.ToString();
            }

            // Рух по сусіднім клітинам
            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dRow[i];
                int newCol = col + dCol[i];

                // Перевіряємо, чи є клітина в межах лабіринту
                if (newRow < 0 || newRow >= R || newCol < 0 || newCol >= C)
                    continue;

                char currentCell = maze[newRow, newCol];

                // Якщо клітина - стіна, пропускаємо
                if (currentCell == 'X')
                    continue;

                // Якщо клітина містить двері, перевіряємо чи є потрібний ключ
                if (currentCell == 'R' || currentCell == 'G' || currentCell == 'B' || currentCell == 'Y')
                {
                    int keyIndex = currentCell switch
                    {
                        'R' => 0,
                        'G' => 1,
                        'B' => 2,
                        'Y' => 3,
                        _ => -1
                    };

                    if ((keyMask & (1 << keyIndex)) == 0)
                        continue;  // Якщо немає відповідного ключа, пропускаємо
                }

                // Якщо клітина містить ключ, додаємо його в набір ключів
                int newKeyMask = keyMask;
                if (currentCell == 'R') newKeyMask |= 1 << 0;
                if (currentCell == 'G') newKeyMask |= 1 << 1;
                if (currentCell == 'B') newKeyMask |= 1 << 2;
                if (currentCell == 'Y') newKeyMask |= 1 << 3;

                // Обчислюємо нову вартість з урахуванням придбання нового ключа
                int newCost = cost;
                if (currentCell == 'R' && (keyMask & 1) == 0) newCost += keyCosts[0];
                if (currentCell == 'G' && (keyMask & 2) == 0) newCost += keyCosts[1];
                if (currentCell == 'B' && (keyMask & 4) == 0) newCost += keyCosts[2];
                if (currentCell == 'Y' && (keyMask & 8) == 0) newCost += keyCosts[3];

                // Якщо ми ще не відвідували цю клітину з таким набором ключів
                if (!visited[newRow, newCol, newKeyMask])
                {
                    visited[newRow, newCol, newKeyMask] = true;
                    queue.Enqueue((newRow, newCol, newKeyMask, newCost));
                }
            }
        }

        // Якщо ми не знайшли шляху до виходу
        return "Sleep";
    }
}

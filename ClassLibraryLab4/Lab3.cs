namespace ClassLibraryLab4
{
    public class Lab3
    {
        public static void ExecuteLab3(string inputFile, string outputFile)
        {
            try
            {
                // Перевірка наявності файлу
                if (!File.Exists(inputFile))
                    throw new FileNotFoundException($"Input file not found: {inputFile}");

                // Читання вхідних даних
                string[] lines = File.ReadAllLines(inputFile);
                var dimensions = lines[0].Split();
                int R = int.Parse(dimensions[0]);
                int C = int.Parse(dimensions[1]);
                int[] keyCosts = Array.ConvertAll(lines[1].Split(), int.Parse);

                // Ініціалізація лабіринту
                char[,] maze = new char[R, C];
                (int, int) start = (0, 0);
                (int, int) end = (0, 0);

                for (int i = 0; i < R; i++)
                {
                    string row = lines[i + 2];
                    for (int j = 0; j < C; j++)
                    {
                        maze[i, j] = row[j];
                        if (row[j] == 'S') start = (i, j);
                        if (row[j] == 'E') end = (i, j);
                    }
                }

                // Запуск BFS
                string result = BFS(maze, R, C, start, end, keyCosts);

                // Запис результату у вихідний файл
                File.WriteAllText(outputFile, result);
                Console.WriteLine("Result written to output file.");
            }
            catch (Exception ex)
            {
                // Обробка помилок
                File.WriteAllText(outputFile, "Error: " + ex.Message);
            }
        }

        public static string BFS(char[,] maze, int R, int C, (int, int) start, (int, int) end, int[] keyCosts)
        {
            var queue = new Queue<(int row, int col, int keyMask, int cost)>();
            bool[,,] visited = new bool[R, C, 16];  // 16 = 2^4 для всіх комбінацій ключів

            queue.Enqueue((start.Item1, start.Item2, 0, 0));
            visited[start.Item1, start.Item2, 0] = true;

            int[] dRow = { -1, 1, 0, 0 };
            int[] dCol = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var (row, col, keyMask, cost) = queue.Dequeue();

                if ((row, col) == end)
                    return cost.ToString();

                for (int i = 0; i < 4; i++)
                {
                    int newRow = row + dRow[i];
                    int newCol = col + dCol[i];

                    if (newRow < 0 || newRow >= R || newCol < 0 || newCol >= C)
                        continue;

                    char currentCell = maze[newRow, newCol];

                    if (currentCell == 'X') continue;

                    int newKeyMask = keyMask;
                    int newCost = cost;

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
                            continue;
                    }

                    if (currentCell == 'R') newKeyMask |= 1 << 0;
                    if (currentCell == 'G') newKeyMask |= 1 << 1;
                    if (currentCell == 'B') newKeyMask |= 1 << 2;
                    if (currentCell == 'Y') newKeyMask |= 1 << 3;

                    if (currentCell == 'R' && (keyMask & 1) == 0) newCost += keyCosts[0];
                    if (currentCell == 'G' && (keyMask & 2) == 0) newCost += keyCosts[1];
                    if (currentCell == 'B' && (keyMask & 4) == 0) newCost += keyCosts[2];
                    if (currentCell == 'Y' && (keyMask & 8) == 0) newCost += keyCosts[3];

                    if (!visited[newRow, newCol, newKeyMask])
                    {
                        visited[newRow, newCol, newKeyMask] = true;
                        queue.Enqueue((newRow, newCol, newKeyMask, newCost));
                    }
                }
            }

            return "Sleep";
        }

    }
}

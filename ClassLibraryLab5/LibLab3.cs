namespace ClassLibraryLab5
{
    public class LibLab3
    {
        static char[,] maze;
        static int R, C;
        static int[] keyCosts = new int[4]; // Вартість ключів: R, G, B, Y
        static (int, int) start, end;

        static int[] dRow = { -1, 1, 0, 0 }; // Напрями руху: вгору, вниз, ліво, право
        static int[] dCol = { 0, 0, -1, 1 };

        public static string ExecuteLab3(string input)
        {
            // Розділяємо рядок на частини
            var parts = input.Split(' ');
            R = int.Parse(parts[0]); // Кількість рядків
            C = int.Parse(parts[1]); // Кількість стовпців

            // Зчитуємо вартість ключів
            for (int i = 0; i < 4; i++)
            {
                keyCosts[i] = int.Parse(parts[2 + i]);
            }

            // Залишок — лабіринт у вигляді рядка
            string mazeString = parts[6];

            // Ініціалізуємо лабіринт
            maze = new char[R, C];
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    maze[i, j] = mazeString[i * C + j];
                    if (maze[i, j] == 'S') start = (i, j);
                    if (maze[i, j] == 'E') end = (i, j);
                }
            }

            // Викликаємо BFS для пошуку шляху
            return BFS();
        }

        static string BFS()
        {
            var queue = new Queue<(int row, int col, int keyMask, int cost)>();
            bool[,,] visited = new bool[R, C, 16]; // Відвідані клітини з різними наборами ключів

            queue.Enqueue((start.Item1, start.Item2, 0, 0));
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

                    // Перевіряємо, чи клітина в межах лабіринту
                    if (newRow < 0 || newRow >= R || newCol < 0 || newCol >= C)
                        continue;

                    char currentCell = maze[newRow, newCol];

                    // Якщо клітина - стіна, пропускаємо
                    if (currentCell == 'X')
                        continue;

                    // Якщо клітина містить двері, перевіряємо наявність ключа
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
                            continue; // Якщо немає ключа, пропускаємо
                    }

                    // Якщо клітина містить ключ, додаємо його в набір ключів
                    int newKeyMask = keyMask;
                    if (currentCell == 'R') newKeyMask |= 1 << 0;
                    if (currentCell == 'G') newKeyMask |= 1 << 1;
                    if (currentCell == 'B') newKeyMask |= 1 << 2;
                    if (currentCell == 'Y') newKeyMask |= 1 << 3;

                    // Обчислюємо нову вартість
                    int newCost = cost;
                    if (currentCell == 'R' && (keyMask & 1) == 0) newCost += keyCosts[0];
                    if (currentCell == 'G' && (keyMask & 2) == 0) newCost += keyCosts[1];
                    if (currentCell == 'B' && (keyMask & 4) == 0) newCost += keyCosts[2];
                    if (currentCell == 'Y' && (keyMask & 8) == 0) newCost += keyCosts[3];

                    // Якщо не відвідували цю клітину з таким набором ключів
                    if (!visited[newRow, newCol, newKeyMask])
                    {
                        visited[newRow, newCol, newKeyMask] = true;
                        queue.Enqueue((newRow, newCol, newKeyMask, newCost));
                    }
                }
            }

            // Якщо шлях не знайдено
            return "Sleep";
        }
    }
}

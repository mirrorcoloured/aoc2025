using Helpers;

namespace Day04
{
    public static class Shared
    {
        // public const string InputFilePath = "day04/sample.txt";
        public const string InputFilePath = "day04/input.txt";

        public static int[,] BuildMatrix(IEnumerable<string> lines)
        {
            int[,] matrix = new int[0, 0];
            int lineNumber = 0;
            foreach (string line in lines)
            {
                if (lineNumber == 0)
                {
                    matrix = new int[line.Length, line.Length];
                }
                for (int charNumber = 0; charNumber < line.Length; charNumber++)
                {
                    char c = line[charNumber];
                    if (c == '.')
                    {
                        matrix[charNumber, lineNumber] = 0;
                    } else if (c == '@')
                    {
                        matrix[charNumber, lineNumber] = 1;
                    }
                }
                lineNumber++;
            }
            return matrix;
        }
    }

    public static class SolutionOne
    {
        public static int Run()
        {
            int[,] matrix = Shared.BuildMatrix(FileHelper.ReadLinesByNewline(Shared.InputFilePath));
            int maxX =  matrix.GetLength(0);
            int maxY = matrix.GetLength(1);
            int AccessibleRolls = 0;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (matrix[y, x] == 0)
                    {
                        continue;
                    }
                    int AdjacentRolls = 0;
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            if (dx == 0 && dy == 0) { continue; }
                            int newX = x + dx;
                            int newY = y + dy;
                            if (newX >= 0 && newX < maxX && newY >= 0 && newY < maxY)
                            {
                                AdjacentRolls += matrix[newY, newX];
                            }
                        }
                    }
                    if (AdjacentRolls < 4)
                    {
                        AccessibleRolls++;
                    }
                }
            }

            return AccessibleRolls;
        }
    }

    public static class SolutionTwo
    {
        public static int Run()
        {
            int[,] matrix = Shared.BuildMatrix(FileHelper.ReadLinesByNewline(Shared.InputFilePath));
            int maxX =  matrix.GetLength(0);
            int maxY = matrix.GetLength(1);
            bool TryRemoveRolls = true;
            int NumRollsRemoved = 0;

            while (TryRemoveRolls)
            {
                TryRemoveRolls = false;
                List<int[]> AccessibleRolls = [];
                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        if (matrix[y, x] == 0)
                        {
                            continue;
                        }
                        int AdjacentRolls = 0;
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                if (dx == 0 && dy == 0) { continue; }
                                int newX = x + dx;
                                int newY = y + dy;
                                if (newX >= 0 && newX < maxX && newY >= 0 && newY < maxY)
                                {
                                    AdjacentRolls += matrix[newY, newX];
                                }
                            }
                        }
                        if (AdjacentRolls < 4)
                        {
                            int[] pos = { x, y };
                            AccessibleRolls.Add(pos);
                        }
                    }
                }
                for (int i = 0; i < AccessibleRolls.Count; i++)
                {
                    int x = AccessibleRolls[i][0];
                    int y = AccessibleRolls[i][1];
                    matrix[y, x] = 0;
                    TryRemoveRolls = true;
                    NumRollsRemoved++;
                }
            }

            return NumRollsRemoved;
        }
    }
}
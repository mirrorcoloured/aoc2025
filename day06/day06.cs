namespace Day06
{
    public static class Shared
    {
        // public const string InputFilePath = "day06/sample.txt";
        public const string InputFilePath = "day06/input.txt";
    }

    public static class SolutionOne
    {
        public static string[,] BuildMatrix(string filePath)
        {
            string content = File.ReadAllText(filePath);
            content = System.Text.RegularExpressions.Regex.Replace(content, @"[ ]+", " ");
            string[] lines = content.Split("\r\n").Select(line => line.Trim()).ToArray();
            int lineCount = lines.Length;
            long colCount = lines[0].Split(' ').Length;

            string[,] matrix = new string[lineCount, colCount];
            for (int i = 0; i < lineCount; i++)
            {
                string line = lines[i];
                string[] values = line.Split(' ');
                for (int j = 0; j < values.Length; j++)
                {
                    matrix[i, j] = values[j];
                }
            }
            return matrix;
        }
        public static long Run()
        {
            string[,] matrix = BuildMatrix(Shared.InputFilePath);
            int NumRows = matrix.GetLength(0);
            int NumCols = matrix.GetLength(1);

            long WorksheetTotal = 0;
            for (int j = 0; j < NumCols; j++)
            {
                string operatorChar = matrix[NumRows-1, j];
                long ColumnTotal =  operatorChar == "+" ? 0 : 1;
                for (int i = 0; i < NumRows - 1; i++)
                {
                    if (operatorChar == "+")
                    {
                        ColumnTotal += Convert.ToInt64(matrix[i, j]);
                    }
                    else if (operatorChar == "*")
                    {
                        ColumnTotal *= Convert.ToInt64(matrix[i, j]);
                    }
                }
                WorksheetTotal += ColumnTotal;
            }

            return WorksheetTotal;
        }
    }

    public static class SolutionTwo
    {
        public static string[,] BuildMatrix(string filePath)
        {
            string content = File.ReadAllText(filePath);
            string[] lines = content.Split("\r\n");
            int lineCount = lines.Length;
            int charCount = lines[0].Length;

            // detect totally blank columns to separate math problems
            List<int> ColumnBreakIndexes = [];
            ColumnBreakIndexes.Add(-1);
            for (int i = 0; i < charCount; i++)
            {
                bool AllEmpty = true;
                for (int j = 0; j < lineCount - 1; j++)
                {
                    if (lines[j][i] != ' ')
                    {
                        AllEmpty = false;
                        break;
                    }
                }
                if (AllEmpty)
                {
                    ColumnBreakIndexes.Add(i);
                }
            }
            ColumnBreakIndexes.Add(charCount);

            string[,] matrix = new string[lineCount, ColumnBreakIndexes.Count - 1];
            for (int ProblemIndex = 0; ProblemIndex < ColumnBreakIndexes.Count - 1; ProblemIndex++)
            {
                int StartCharacterIndex = ColumnBreakIndexes[ProblemIndex] + 1;
                int EndCharacterIndex = ColumnBreakIndexes[ProblemIndex + 1] - 1;

                for (int j = EndCharacterIndex; j >= StartCharacterIndex; j--)
                {
                    int NumberIndex = EndCharacterIndex - j;
                    string colString = "";
                    for (int k = 0; k < lineCount - 1; k++)
                    {
                        if (lines[k][j] != ' ')
                        {
                            colString += lines[k][j];
                        }
                    }
                    matrix[NumberIndex, ProblemIndex] = colString;
                }
                matrix[lineCount - 1, ProblemIndex] = lines[lineCount - 1][StartCharacterIndex..EndCharacterIndex].Trim();
            }
            return matrix;
        }
        public static long Run()
        {
            string[,] matrix = BuildMatrix(Shared.InputFilePath);
            int NumRows = matrix.GetLength(0);
            int NumCols = matrix.GetLength(1);

            long WorksheetTotal = 0;
            for (int j = 0; j < NumCols; j++)
            {
                string operatorChar = matrix[NumRows-1, j];
                long ColumnTotal =  operatorChar == "+" ? 0 : 1;
                for (int i = 0; i < NumRows - 1; i++)
                {
                    if (matrix[i, j] == null || matrix[i, j] == "")
                    {
                        continue;
                    }
                    if (operatorChar == "+")
                    {
                        ColumnTotal += Convert.ToInt64(matrix[i, j]);
                    }
                    else if (operatorChar == "*")
                    {
                        ColumnTotal *= Convert.ToInt64(matrix[i, j]);
                    }
                }
                WorksheetTotal += ColumnTotal;
            }

            return WorksheetTotal;
        }
    }
}
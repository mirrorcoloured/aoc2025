using Helpers;

namespace Day07
{
    public static class Shared
    {
        // public const string InputFilePath = "day07/sample.txt";
        public const string InputFilePath = "day07/input.txt";
    }

    public static class SolutionOne
    {
        public static int Run()
        {
            string[] lines = FileHelper.ReadLinesByNewline(Shared.InputFilePath).ToArray();
            int LineLength = lines[0].Length;
            int[] BeamIndexes = new int[LineLength];
            int TotalSplits = 0;

            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == 'S')
                    {
                        BeamIndexes[i] = 1;
                    }
                    else if (line[i] == '^' && BeamIndexes[i] == 1)
                    {
                        BeamIndexes[i] = 0;
                        BeamIndexes[i+1] = 1;
                        BeamIndexes[i-1] = 1;
                        TotalSplits++;
                    }
                }
            }

            return TotalSplits;
        }
    }

    public static class SolutionTwo
    {
        public static long Run()
        {
            string[] lines = FileHelper.ReadLinesByNewline(Shared.InputFilePath).ToArray();
            int LineLength = lines[0].Length;
            long[] BeamSplits = new long[LineLength];
            long[] NextBeamSplits;
            int TotalSplits = 0;

            foreach (string line in lines)
            {
                NextBeamSplits = new long[LineLength];
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == 'S')
                    {
                        NextBeamSplits[i] = 1;
                    }
                    else if (line[i] == '.')
                    {
                        NextBeamSplits[i] += BeamSplits[i];
                    }
                    else if (line[i] == '^' && BeamSplits[i] > 0)
                    {
                        NextBeamSplits[i] = 0;
                        NextBeamSplits[i+1] += BeamSplits[i];
                        NextBeamSplits[i-1] += BeamSplits[i];
                        TotalSplits++;
                    }
                }
                for (int i = 0; i < LineLength; i++)
                {
                    BeamSplits[i] = NextBeamSplits[i];
                }
            }

            return BeamSplits.Sum();
        }
    }
}
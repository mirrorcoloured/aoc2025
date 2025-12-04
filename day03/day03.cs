using Helpers;

namespace Day03
{
    public static class Constants
    {
        // public const string InputFilePath = "day03/sample.txt";
        public const string InputFilePath = "day03/input.txt";
    }

    public static class SolutionOne
    {
        public static int Run()
        {
            int rollingSum = 0;
            foreach (string line in FileHelper.ReadLinesByNewline(Constants.InputFilePath))
            {
                List<int> digits = line.Select(c => int.Parse(c.ToString())).ToList();
                int maxTens = digits.SkipLast(1).Max();
                int firstIndex = digits.IndexOf(maxTens);
                int maxOnes = digits.Skip(firstIndex + 1).Max();
                rollingSum += (10 * maxTens) + maxOnes;
            }

            return rollingSum;
        }
    }

    public static class SolutionTwo
    {
        public static long Run()
        {
            long rollingSum = 0;
            const int MAXDIGITS = 12;
            foreach (string line in FileHelper.ReadLinesByNewline(Constants.InputFilePath))
            {
                List<int> digits = line.Select(c => int.Parse(c.ToString())).ToList();
                List<int> chosenDigits = [];
                long thisSum = 0;
                for (int digitsToGo = MAXDIGITS; digitsToGo > 0; digitsToGo--)
                {
                    int maxDigit = digits.SkipLast(digitsToGo-1).Max();
                    int firstIndex = digits.IndexOf(maxDigit);
                    digits.RemoveRange(0, firstIndex + 1);
                    thisSum += maxDigit * (long)Math.Pow(10, digitsToGo - 1);
                }
                rollingSum += thisSum;
            }

            return rollingSum;
        }
    }
}
using Helpers;

namespace Day02
{
    public static class Constants
    {
        // public const string InputFilePath = "day02/sample.txt";
        public const string InputFilePath = "day02/input.txt";
    }

    public static class SolutionOne
    {
        public static long Run()
        {
            List<long> invalidIDs = [];

            foreach (string line in FileHelper.ReadLinesByCommas(Constants.InputFilePath))
            {
                long firstValue = long.Parse(line.Split("-")[0]);
                long secondValue = long.Parse(line.Split("-")[1]);
                for (long i = firstValue; i <= secondValue; i++)
                {
                    string valueStr = i.ToString();
                    if (!IsValidID(valueStr))
                    {
                        invalidIDs.Add(i);
                    }
                }
            }

            return invalidIDs.Sum();
        }

        private static bool IsValidID(string id)
        {
            if (id.Length % 2 != 0) { return true; }
            string firstHalf = id.Substring(0, id.Length / 2);
            string secondHalf = id.Substring(id.Length / 2);
            if (firstHalf != secondHalf) { return true; }
            return false;
        }
    }

    public static class SolutionTwo
    {
        public static long Run()
        {
            List<long> invalidIDs = [];

            foreach (string line in FileHelper.ReadLinesByCommas(Constants.InputFilePath))
            {
                long firstValue = long.Parse(line.Split("-")[0]);
                long secondValue = long.Parse(line.Split("-")[1]);
                for (long i = firstValue; i <= secondValue; i++)
                {
                    string valueStr = i.ToString();
                    if (!IsValidID(valueStr))
                    {
                        invalidIDs.Add(i);
                    }
                }
            }

            return invalidIDs.Sum();
        }

        private static bool IsValidID(string id)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(.*)\1+$");
            return !regex.IsMatch(id);
        }
    }
}
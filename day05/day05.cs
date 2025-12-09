using Helpers;

namespace Day05
{
    public static class Shared
    {
        // public const string InputFilePath = "day05/sample.txt";
        public const string InputFilePath = "day05/input.txt";
    }
    public class Range
    {
        public long Min { get; set; }
        public long Max { get; set; }

        public Range(long min, long max)
        {
            Min = min;
            Max = max;
        }

        public bool Contains(long value)
        {
            return value >= Min && value <= Max;
        }

        public bool Overlaps(Range other)
        {
            return this.Min <= other.Max && other.Min <= this.Max;
        }

        public Range Merge(Range other)
        {
            return new Range(Math.Min(this.Min, other.Min), Math.Max(this.Max, other.Max));
        }

        public long Size()
        {
            return Max - Min + 1;
        }
    }
    public static class SolutionOne
    {
        public static long Run()
        {
            List<Range> FreshRanges = [];
            List<long> IngredientIDs = [];
            long NumberOfFreshIngredients = 0;

            foreach (string line in FileHelper.ReadLinesByNewline(Shared.InputFilePath))
            {
                if (line.IndexOf('-') > -1)
                {
                    string[] parts = line.Split('-');
                    FreshRanges.Add(new Range(long.Parse(parts[0]), long.Parse(parts[1])));
                }
                else if (line.Length > 0)
                {
                    IngredientIDs.Add(long.Parse(line));
                }
            }

            foreach (long IngredientID in IngredientIDs)
            {
                foreach (Range range in FreshRanges)
                    {
                        if (range.Contains(IngredientID))
                        {
                            NumberOfFreshIngredients++;
                            break;
                        }
                    }
            }

            return NumberOfFreshIngredients;
        }
    }

    public static class SolutionTwo
    {
        public static long Run()
        {
            List<Range> FreshRanges = [];

            foreach (string line in FileHelper.ReadLinesByNewline(Shared.InputFilePath))
            {
                if (line.IndexOf('-') > -1)
                {
                    string[] parts = line.Split('-');
                    Range newRange = new Range(long.Parse(parts[0]), long.Parse(parts[1]));
                    bool CheckForOverlaps = true;
                    while (CheckForOverlaps)
                    {
                        CheckForOverlaps = false;
                        foreach (Range range in FreshRanges)
                        {
                            if (newRange.Overlaps(range))
                            {
                                FreshRanges.Remove(range);
                                newRange = range.Merge(newRange);
                                CheckForOverlaps = true;
                                break;
                            }
                        }
                    }
                    FreshRanges.Add(newRange);
                }
                else
                {
                    break;
                }
            }

            long TotalFreshIDs = FreshRanges.Sum(r => r.Size());

            return TotalFreshIDs;
        }
    }
}
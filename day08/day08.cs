using Helpers;

namespace Day08
{
    public static class Shared
    {
        public const string InputFilePath = "day08/sample.txt";
        // public const string InputFilePath = "day08/input.txt";
    }
    public class JunctionBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public JunctionBox(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }
        public float distanceTo(JunctionBox other)
        {
            return (float)Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2) + Math.Pow(other.Z - Z, 2));
        }
    }
    public class Circuit
    {
        public List<JunctionBox> JunctionBoxes { get; set; }

        public Circuit()
        {
            JunctionBoxes = new List<JunctionBox>();
        }

        public Circuit Combine(Circuit other)
        {
            Circuit combined = new Circuit();
            combined.JunctionBoxes.AddRange(this.JunctionBoxes);
            combined.JunctionBoxes.AddRange(other.JunctionBoxes);
            return combined;
        }
    }
    public readonly struct Entry : IComparable<Entry>
    {
        public int I { get; }
        public int J { get; }
        public float Value { get; }

        public Entry(int i, int j, float value) => (I, J, Value) = (i, j, value);

        public int CompareTo(Entry other) => Value.CompareTo(other.Value);
    }
    public static class PairSorter
{
        public static IEnumerable<Entry> SortPairsByValue(List<Entry> entries, bool ascending = true)
        {
            if (ascending)
                entries.Sort((a, b) => a.Value.CompareTo(b.Value));
            else
                entries.Sort((a, b) => b.Value.CompareTo(a.Value));

            foreach (var e in entries)
                yield return e;
        }
    }
    public static class SolutionOne
    {
        const int MaxConnections = 10;
        // const int MaxConnections = 1000;
        public static int Run()
        {
            List<JunctionBox> JunctionBoxes = [];
            List<Circuit> Circuits = [];
            
            // construct junction boxes
            foreach (string line in FileHelper.ReadLinesByNewline(Shared.InputFilePath))
            {
                string[] parts = line.Split(",");
                JunctionBox box = new JunctionBox(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
                JunctionBoxes.Add(box);
                Circuit circuit = new Circuit();
                circuit.JunctionBoxes.Add(box);
                Circuits.Add(circuit);
            }

            // compute all distances
            List<Entry> distanceEntries = [];
            for (int i = 0; i < JunctionBoxes.Count; i++) {
                for (int j = i + 1; j < JunctionBoxes.Count; j++) {
                    if (i == j) continue;
                    float distance = JunctionBoxes[i].distanceTo(JunctionBoxes[j]);
                    distanceEntries.Add(new Entry(i, j, distance));
                }
            }

            // step through sorted distances
            var sortedPairs = PairSorter.SortPairsByValue(distanceEntries, ascending: true);
            int maxPairs = Math.Min(MaxConnections, distanceEntries.Count);
            for (int i = 0; i < maxPairs; i++)
            {
                Entry Pair = sortedPairs.Skip(i).First();
                Console.WriteLine(Pair.I + " " + Pair.J + " => " + JunctionBoxes[Pair.I].ToString() + " " + JunctionBoxes[Pair.J].ToString() + " : " + Pair.Value);
                // combine circuits
            }

            return 0;
        }
    }

    public static class SolutionTwo
    {
        public static int Run()
        {
            foreach (string line in FileHelper.ReadLinesByNewline(Shared.InputFilePath))
            {
                
            }

            return 0;
        }
    }
}
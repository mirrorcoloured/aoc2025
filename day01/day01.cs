using Helpers;

namespace Day01
{
    public static class Constants
    {
        // public const string InputFilePath = "day01/sample.txt";
        public const string InputFilePath = "day01/input.txt";
        public const int MaxPosition = 99;
    }
    public static class SolutionOne
    {
        public static int Run()
        {
            int position = 50;
            int zeroCounter = 0;

            foreach (string line in FileHelper.ReadLinesByNewline(Constants.InputFilePath))
            {
                char direction = line[0];
                int distance = int.Parse(line.Substring(1));
                
                switch (direction)
                {
                    case 'L':
                        position -= distance;
                        break;
                    case 'R':
                        position += distance;
                        break;
                    default:
                        throw new Exception("Invalid direction: " + direction);
                }
                position = (position + (Constants.MaxPosition + 1)) % (Constants.MaxPosition + 1);

                if (position == 0) { zeroCounter++; }
            }

            return zeroCounter;
        }
    }

    public static class SolutionTwo
    {
        public static int Run()
        {
            int position = 50;
            int zeroCounter = 0;

            // decode hex
            long secretValue = 0x434C49434B;
            Console.WriteLine("Secret value: " + secretValue);

            foreach (string line in FileHelper.ReadLinesByNewline(Constants.InputFilePath))
            {
                char direction = line[0];
                int distance = int.Parse(line.Substring(1));

                for (int i = 0; i < distance; i++)
                {
                    switch (direction)
                    {
                        case 'L':
                            position--;
                            break;
                        case 'R':
                            position++;
                            break;
                        default:
                            throw new Exception("Invalid direction: " + direction);
                    }
                    position = (position + (Constants.MaxPosition + 1)) % (Constants.MaxPosition + 1);

                    if (position == 0) { zeroCounter++; }
                }
            }

            return zeroCounter;
        }
    }
}
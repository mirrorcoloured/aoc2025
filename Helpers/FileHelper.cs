namespace Helpers
{
    public static class FileHelper
    {
        public static IEnumerable<string> ReadLinesByNewline(string filePath)
        {
            foreach (string line in File.ReadLines(filePath))
            {
                yield return line;
            }
        }
        
        public static IEnumerable<string> ReadLinesByCommas(string filePath)
        {
            string content = File.ReadAllText(filePath);
            string[] parts = content.Split(',');
            foreach (string part in parts)
            {
                yield return part;
            }
        }
    }
}
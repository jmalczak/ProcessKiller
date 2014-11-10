namespace ProcessKiller.Logic
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileReader
    {
        private const string CommentString = "#";

        private readonly string filePath;

        public FileReader(string filePath)
        {
            this.filePath = filePath;
        }

        public List<string> ReadProcessesToKill()
        {
            var allLines = File.ReadAllLines(this.filePath);
            return allLines.Where(l => !l.StartsWith(CommentString) && l != string.Empty)
                           .Select(l => l.ToLower())
                           .ToList();
        }
    }
}
namespace ProcessKiller.Logic
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using global::ProcessKiller.Model;

    public class FileReader
    {
        private const string CommentString = "#";
        private const char SplitString = ';';
        private const int ProcessNameIndex = 0;
        private const int ProcessKillDelayInSecondsIndex = 1;

        private readonly string filePath;

        public FileReader(string filePath)
        {
            this.filePath = filePath;
        }

        public List<KillModel> ReadProcessesToKill()
        {
            var killConfiguration = this.ReadConfiguration();
            var result = new List<KillModel>();

            foreach (var configuration in killConfiguration)
            {
                result.Add(ParseConfigurationLine(configuration));
            }

            return result;
        }

        private static KillModel ParseConfigurationLine(string configuration)
        {
            var separatedValues = configuration.Split(SplitString);
            var killModel = new KillModel { ProcessName = separatedValues[ProcessNameIndex] };

            if (separatedValues.Length > 1)
            {
                int processKillDelayInSeconds;
                if (int.TryParse(separatedValues[ProcessKillDelayInSecondsIndex], out processKillDelayInSeconds))
                {
                    killModel.ProcessKillDelayInSeconds = processKillDelayInSeconds;
                }
            }

            return killModel;
        }

        private List<string> ReadConfiguration()
        {
            var allLines = File.ReadAllLines(this.filePath);
            return allLines.Where(l => !l.StartsWith(CommentString) && l != string.Empty)
                           .Select(l => l.ToLower())
                           .ToList();
        } 
    }
}
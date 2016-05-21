using System;

namespace SmartSubtitlesRenamer
{
    public interface ILogger
    {
        void LogStart(string currDir);
        void LogEnd();
        void LogRename(string oldName, string newName);
    }

    public class ConsoleLogger : ILogger
    {
        public void LogStart(string currDir)
        {
            Console.WriteLine($"[{DateTime.Now}] Subtitles renamer START ({currDir})");
        }

        public void LogEnd()
        {
            Console.WriteLine($"[{ DateTime.Now}] Subtitles renamer END");

        }

        public void LogRename(string oldName, string newName)
        {
            Console.WriteLine($"*** Subtitle file found and renamed [{oldName}] ---> [{newName}]");
        }
    }
}

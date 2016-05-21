using System.IO;
using System.Linq;

namespace SmartSubtitlesRenamer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var filesProvider = new FilesInCurrentDirectoryProvider();
            var consoleLogger = new ConsoleLogger();
            var subtitleRenamer = new SubtitleRenamer(filesProvider, consoleLogger);

            consoleLogger.LogStart(Directory.GetCurrentDirectory());

            var files = filesProvider.GetFiles();

            var allSplitted = files.Select(FileNameAndExtensionsSplitter.Split);

            foreach (var splitted in allSplitted.Where(x => x.Extension.IsMediaExtension()))
            {
                subtitleRenamer.RenameCorrespondingSubtitle(splitted, allSplitted);
            }

            consoleLogger.LogEnd();
        }
    }
}

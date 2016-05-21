using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartSubtitlesRenamer
{
    public interface ISubtitleRenamer
    {
        void RenameCorrespondingSubtitle(FileNameAndExtension fileName, IEnumerable<FileNameAndExtension> allFiles);
    }

    public class SubtitleRenamer : ISubtitleRenamer
    {
        private readonly Regex _standardPattern = new Regex("[Ss]\\d\\d[Ee]\\d\\d");
        private readonly Regex _xPattern = new Regex("\\d\\d[Xx]\\d\\d");

        private readonly IFilesInCurrentDirectoryProvider _filesProvider;
        private readonly ILogger _logger;

        public SubtitleRenamer(IFilesInCurrentDirectoryProvider filesProvider, ILogger logger)
        {
            _filesProvider = filesProvider;
            _logger = logger;
        }

        public void RenameCorrespondingSubtitle(FileNameAndExtension fileName, IEnumerable<FileNameAndExtension> allFiles)
        {
            var match = Match(_standardPattern, fileName.FullName);

            if (!match.Success)
            {
                match = Match(_xPattern, fileName.FullName);

                if (!match.Success)
                {
                    return;
                }
            }

            var subtitles = GetSubtitleFilesByMatch(allFiles, match.Value);

            if (subtitles.Count == 0)
            {
                // Sometimes subtitles name contains AxB instead of SAEB
                var fallbackMatch = match.Value.Substring(1).Replace("E", "x");
                subtitles = GetSubtitleFilesByMatch(allFiles, fallbackMatch);
            }

            if (subtitles.Count == 0 || subtitles.Count > 1)
            {
                return;
            }

            var subtitle = subtitles.Single();
            var newName = $"{fileName.FileName}.{ExtensionsConsts.Txt}";

            _filesProvider.Rename(subtitle.FullName, newName);

            _logger.LogRename(subtitle.FullName, newName);
        }

        private List<FileNameAndExtension> GetSubtitleFilesByMatch(IEnumerable<FileNameAndExtension> allFiles, string matchValue)
        {
            return allFiles
                .Where(x => x.FullName.Contains(matchValue))
                .Where(x => x.Extension.IsSubtitlesExtension())
                .ToList();
        }

        private Match Match(Regex regex, string fileName)
        {
            return regex.Match(fileName);
        }
    }
}

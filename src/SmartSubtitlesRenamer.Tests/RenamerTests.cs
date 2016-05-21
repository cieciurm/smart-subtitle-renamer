using System.Linq;
using Moq;
using NFluent;
using NUnit.Framework;

namespace SmartSubtitlesRenamer.Tests
{
    public class RenamerTests
    {
        private IFilesInCurrentDirectoryProvider _filesProvider;
        private ILogger _logger;

        private readonly string[] _files = { "name s02e07.avi", "subtitles s02e07.srt" };

        [SetUp]
        public void SetUp()
        {
            var providerMock = new Mock<IFilesInCurrentDirectoryProvider>();

            providerMock
                .Setup(x => x.GetFiles())
                .Returns(_files);

            providerMock
                .Setup(x => x.Rename(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((x, y) =>
                {
                    for (var i = 0; i < _files.Length; i++)
                    {
                        if (_files[i] == x) _files[i] = y;
                    }

                    return y;
                });

            _filesProvider = providerMock.Object;
            _logger = new Mock<ILogger>().Object;
        }

        [Test]
        public void RenameCorrespondingSubtitle_EndToEndTestWithMocks_ShouldSucceed()
        {
            var renamer = new SubtitleRenamer(_filesProvider, _logger);

            var files = _filesProvider.GetFiles();
            var splitted = files.Select(FileNameAndExtensionsSplitter.Split);

            foreach (var mediaFile in splitted.Where(x => x.Extension.IsMediaExtension()))
            {
                renamer.RenameCorrespondingSubtitle(mediaFile, splitted);
            }

            Check.That(files.Count(x => x.EndsWith(ExtensionsConsts.Srt))).IsEqualTo(0);
            Check.That(files[1]).IsEqualTo("name s02e07.txt");
        }

    }
}

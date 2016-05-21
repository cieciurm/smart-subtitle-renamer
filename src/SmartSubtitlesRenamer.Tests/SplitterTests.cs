using NFluent;
using NUnit.Framework;

namespace SmartSubtitlesRenamer.Tests
{
    public class SplitterTests
    {
        [Test]
        public void Split_SimpleName_ShouldSucceed()
        {
            var result = FileNameAndExtensionsSplitter.Split("name.avi");

            Check.That(result.FileName).IsEqualTo("name");
            Check.That(result.Extension).IsEqualTo("avi");
            Check.That(result.FullName).IsEqualTo("name.avi");
        }

        [Test]
        public void Split_NameWithDot_ShouldSucceed()
        {
            var result = FileNameAndExtensionsSplitter.Split("name.something.avi");

            Check.That(result.FileName).IsEqualTo("name.something");
            Check.That(result.Extension).IsEqualTo("avi");
            Check.That(result.FullName).IsEqualTo("name.something.avi");
        }

        [Test]
        public void Split_NameWithDotAndSpaces_ShouldSucceed()
        {
            var result = FileNameAndExtensionsSplitter.Split("name s04 e07-avs.something.avi");

            Check.That(result.FileName).IsEqualTo("name s04 e07-avs.something");
            Check.That(result.Extension).IsEqualTo("avi");
            Check.That(result.FullName).IsEqualTo("name s04 e07-avs.something.avi");
        }
    }
}

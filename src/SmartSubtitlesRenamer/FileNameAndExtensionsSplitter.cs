namespace SmartSubtitlesRenamer
{
    public class FileNameAndExtension
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public string FullName { get; set; }

        public FileNameAndExtension(string fileName, string extension, string fullName)
        {
            FileName = fileName;

            Extension = extension;

            FullName = fullName;
        }
    }

    public static class FileNameAndExtensionsSplitter
    {
        public static FileNameAndExtension Split(string fileNameWithExtension)
        {
            var lastDotIndex = fileNameWithExtension.LastIndexOf(".");

            var fileName = fileNameWithExtension.Substring(0, lastDotIndex);

            var extension = fileNameWithExtension.Substring(lastDotIndex + 1);

            return new FileNameAndExtension(fileName, extension, fileNameWithExtension);
        }
    }
}

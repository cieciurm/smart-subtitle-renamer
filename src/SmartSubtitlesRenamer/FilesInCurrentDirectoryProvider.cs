using System.IO;

namespace SmartSubtitlesRenamer
{
    public interface IFilesInCurrentDirectoryProvider
    {
        string[] GetFiles();

        string Rename(string oldName, string newName);
    }

    public class FilesInCurrentDirectoryProvider : IFilesInCurrentDirectoryProvider
    {
        public string[] GetFiles()
        {
            return Directory.GetFiles(".", "*.*", SearchOption.AllDirectories);
        }

        public string Rename(string oldName, string newName)
        {
            File.Move(oldName, newName);

            return newName;
        }
    }
}

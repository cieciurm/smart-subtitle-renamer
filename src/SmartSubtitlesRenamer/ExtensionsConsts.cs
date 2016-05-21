namespace SmartSubtitlesRenamer
{
    public static class ExtensionsConsts
    {
        public const string Avi = "avi";
        public const string Mkv = "mkv";
        public const string Mp4 = "mp4";

        public const string Srt = "srt";
        public const string Txt = "txt";
    }

    public static class ExtensionsExtensions
    {
        public static bool IsMediaExtension(this string extension)
        {
            return extension.EndsWith(ExtensionsConsts.Avi) ||
                   extension.EndsWith(ExtensionsConsts.Mkv) ||
                   extension.EndsWith(ExtensionsConsts.Mp4);
        }

        public static bool IsSubtitlesExtension(this string extension)
        {
            return extension.EndsWith(ExtensionsConsts.Srt) ||
                   extension.EndsWith(ExtensionsConsts.Txt);
        }
    }
}

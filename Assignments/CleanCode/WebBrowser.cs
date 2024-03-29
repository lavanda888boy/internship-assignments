namespace CleanCode
{
    internal class WebBrowser
    {
        public BrowserName Name { get; set; }
        public int MajorVersion { get; set; }

        public WebBrowser(string name, int majorVersion)
        {
            Name = TranslateStringToBrowserName(name);
            MajorVersion = majorVersion;
        }

        private BrowserName TranslateStringToBrowserName(string name)
        {
            if (name.Contains("IE")) return BrowserName.InternetExplorer;
            return BrowserName.Unknown;
        }

        public enum BrowserName
        {
            Unknown,
            InternetExplorer,
            Firefox,
            Chrome,
            Opera,
            Safari,
            Dolphin,
            Konqueror,
            Linx
        }
    }
}

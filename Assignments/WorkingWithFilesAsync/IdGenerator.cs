namespace WorkingWithFilesAsync
{
    internal class IdGenerator
    {
        private static int _currentId = 0;
        public static int CurrentID
        {
            get
            {
                _currentId++;
                return _currentId;
            }
        }
    }
}

namespace CleanCode
{
    internal class Session
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }

        public Session(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}

namespace SOLID.entity
{
    internal class Notification
    {
        public string Content { get; set; }
        public DateTimeOffset CreationDate { get; private set; }

        public Notification(string content)
        {
            Content = content;
            CreationDate = new DateTimeOffset(DateTime.UtcNow);
        }

        public override string ToString()
        {
            return $"Date: {CreationDate};\nMessage: {Content}\n";
        }
    }
}
